using Data.Models;
using Data.Models.Enums;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _context;

        public OrderRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Order> Orders, int TotalCount)> GetAllAsync(Guid? clientId = null, DateTime? orderDate = null, OrderStatus? status = null, int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Orders.AsQueryable();

            if (clientId.HasValue)
                query = query.Where(o => o.ClientId == clientId.Value);

            if (orderDate.HasValue)
                query = query.Where(o => o.OrdersDateTime >= orderDate.Value);

            if (status.HasValue)
                query = query.Where(o => o.Status == status.Value);

            var totalCount = await query.CountAsync();

            // Пагинация
            var orders = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (orders, totalCount);
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders.Include(o => o.Client).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
