using Data.Models.Enums;
using Data.Models;

namespace Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<(IEnumerable<Order> Orders, int TotalCount)> GetAllAsync(Guid? clientId = null, DateTime? orderDate = null, OrderStatus? status = null, int pageNumber = 1, int pageSize = 10);

        Task<Order?> GetByIdAsync(Guid id);

        Task AddAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(Guid id);
    }

}
