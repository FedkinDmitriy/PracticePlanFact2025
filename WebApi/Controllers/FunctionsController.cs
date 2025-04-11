using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с функциями
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FunctionsController : ControllerBase
    {
        private readonly IFunctionRepository _functionRepository;

        public FunctionsController(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }
        /// <summary>
        /// Получить сумму заказов в день рождения клиента со статусом Выполнен
        /// </summary>
        /// <returns>Список клиентов и сумма выполненных заказов в день рождения</returns>
        [HttpGet("birthday-orders-total")]
        public async Task<IActionResult> GetBirthdayOrdersTotal()
        {
            var result = await _functionRepository.GetBirthdayOrdersTotalAsync();
            return Ok(result);
        }
        /// <summary>
        /// Получить средний чек по каждому часу
        /// </summary>
        /// <returns>Список часов и средний чек для каждого в порядке убывания</returns>
        [HttpGet("avg-order-sum-per-hour")]
        public async Task<IActionResult> GetAvgOrderSumPerHour()
        {
            var result = await _functionRepository.GetAvgOrderSumPerHourAsync();
            return Ok(result);
        }
    }
}
