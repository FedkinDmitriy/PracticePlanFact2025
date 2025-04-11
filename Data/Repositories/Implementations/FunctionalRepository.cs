using Data.Models.Functions;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly AppDBContext _context;

        public FunctionRepository(AppDBContext context)
        {
            _context = context;
        }

        // Метод для вызова функции get_birthday_orders_total
        public async Task<IEnumerable<BirthdayOrderTotal>> GetBirthdayOrdersTotalAsync()
        {
            return await _context.Set<BirthdayOrderTotal>().FromSqlRaw("SELECT * FROM get_birthday_orders_total()").ToListAsync();
        }

        // Метод для вызова функции get_avg_order_sum_per_hour
        public async Task<IEnumerable<AvgOrderSumPerHour>> GetAvgOrderSumPerHourAsync()
        {
            return await _context.Set<AvgOrderSumPerHour>().FromSqlRaw("SELECT * FROM get_avg_order_sum_per_hour()").ToListAsync();
        }
    }
}
