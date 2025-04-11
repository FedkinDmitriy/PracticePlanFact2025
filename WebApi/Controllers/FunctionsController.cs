using Data.Models.Functions;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с аналитическими функциями
    /// Использует <see cref="IFunctionRepository"/> для взаимодействия с базой данных.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FunctionsController : ControllerBase
    {
        private readonly IFunctionRepository _functionRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="FunctionsController"/>
        /// </summary>
        /// <param name="functionRepository">Репозиторий для работы с аналитическими функциями</param>
        public FunctionsController(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }

        /// <summary>
        /// Получить общую сумму заказов, сделанных в день рождения клиентов.
        /// </summary>
        /// <returns>Общая сумма заказов в день рождения</returns>
        [HttpGet("birthday-orders-total")]
        public async Task<IActionResult> GetBirthdayOrdersTotal()
        {
            var result = await _functionRepository.GetBirthdayOrdersTotalAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получить средний чек заказов по каждому часу суток.
        /// </summary>
        /// <returns>Список объектов с информацией о часе и среднем чеке</returns>
        [HttpGet("avg-order-sum-per-hour")]
        public async Task<IActionResult> GetAvgOrderSumPerHour()
        {
            var result = await _functionRepository.GetAvgOrderSumPerHourAsync();
            return Ok(result);
        }
    }
}