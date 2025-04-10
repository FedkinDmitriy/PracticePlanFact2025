using Data.Models;
using Data.Models.Enums;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с заказами
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Получить список заказов с возможностью фильтрации и пагинации
        /// </summary>
        /// <param name="clientId">ID клиента</param>
        /// <param name="orderDate">Дата заказа</param>
        /// <param name="status">Статус заказа</param>
        /// <param name="pageNumber">Номер страницы (по умолчанию 1)</param>
        /// <param name="pageSize">Размер страницы (по умолчанию 10)</param>
        /// <returns>Список заказов и их общее количество</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid? clientId, DateTime? orderDate, OrderStatus? status, int pageNumber = 1, int pageSize = 10)
        {
            var (orders, totalCount) = await _orderRepository.GetAllAsync(clientId, orderDate, status, pageNumber, pageSize);
            return Ok(new { totalCount, orders });
        }

        /// <summary>
        /// Получить заказ по ID
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <returns>Заказ с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        /// <summary>
        /// Создать новый заказ
        /// </summary>
        /// <param name="order">Данные заказа</param>
        /// <returns>Созданный заказ</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            await _orderRepository.AddAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        /// <summary>
        /// Обновить существующий заказ
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <param name="order">Новые данные заказа</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Order order)
        {
            if (id != order.Id)
                return BadRequest();

            await _orderRepository.UpdateAsync(order);
            return NoContent();
        }

        /// <summary>
        /// Удалить заказ по ID
        /// </summary>
        /// <param name="id">ID заказа</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
