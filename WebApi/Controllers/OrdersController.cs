using Data.Models;
using Data.Models.Enums;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с заказами
    /// Использует <see cref="IOrderRepository"/> для взаимодействия с базой данных.
    /// Для работы со статусами заказов используется перечисление <see cref="OrderStatus"/> 
    /// (Не_обработан = 0, Отменен = 1, Выполнен = 2).
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
        /// Получить список заказов с возможностью фильтрации и пагинации.
        /// </summary>
        /// <param name="clientId">ID клиента для фильтрации заказов по клиенту</param>
        /// <param name="orderDate">Дата заказа для фильтрации</param>
        /// <param name="status">Статус заказа для фильтрации (см. <see cref="OrderStatus"/>)</param>
        /// <param name="pageNumber">Номер страницы (по умолчанию 1)</param>
        /// <param name="pageSize">Размер страницы (по умолчанию 10)</param>
        /// <returns>JSON-объект с полями <c>totalCount</c> и <c>orders</c></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid? clientId, DateTime? orderDate, OrderStatus? status, int pageNumber = 1, int pageSize = 10)
        {
            var (orders, totalCount) = await _orderRepository.GetAllAsync(clientId, orderDate, status, pageNumber, pageSize);
            return Ok(new { totalCount, orders });
        }

        /// <summary>
        /// Получить заказ по <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Объект <see cref="Order"/> или статус 404, если заказ не найден</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        /// <summary>
        /// Создать новый заказ.
        /// </summary>
        /// <param name="order">Экземпляр <see cref="Order"/>, содержащий данные для нового заказа</param>
        /// <returns>Результат с данными созданного заказа</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            await _orderRepository.AddAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        /// <summary>
        /// Обновить заказ по <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="order">Новый объект <see cref="Order"/> с обновленными данными</param>
        /// <returns>204 No Content при успешном обновлении</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Order order)
        {
            if (id != order.Id)
                return BadRequest();

            await _orderRepository.UpdateAsync(order);
            return NoContent();
        }

        /// <summary>
        /// Удалить заказ по <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>204 No Content при успешном удалении</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var client = await _orderRepository.GetByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
