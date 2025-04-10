using Data.Models;

namespace Data.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<(IEnumerable<Client> Clients, int TotalCount)> GetAllAsync(string? firstName = null, string? lastName = null, DateOnly? dateOfBirth = null, int pageNumber = 1, int pageSize = 10);
        Task<Client?> GetByIdAsync(Guid id);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Guid id);
    }
}
