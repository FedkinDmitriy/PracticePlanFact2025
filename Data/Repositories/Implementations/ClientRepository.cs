using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDBContext _context;

        public ClientRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Client> Clients, int TotalCount)> GetAllAsync(string? firstName = null, string? lastName = null, DateOnly? dateOfBirth = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Clients.AsQueryable();

           
            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(c => c.FirstName.Contains(firstName));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(c => c.LastName.Contains(lastName));

            if (dateOfBirth.HasValue)
                query = query.Where(c => c.DateOfBirth == dateOfBirth.Value);

            // Получаем общее количество записей
            var totalCount = await query.CountAsync();

            // Пагинация
            var clients = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (clients, totalCount);
        }

        public async Task<Client?> GetByIdAsync(Guid id)
        {
            return await _context.Clients.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
