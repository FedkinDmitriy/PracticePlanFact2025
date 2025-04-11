using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с клиентами.
    /// Использует <see cref="IClientRepository"/> для взаимодействия с базой данных.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// Получить список клиентов с возможностью фильтрации и пагинации.
        /// </summary>
        /// <param name="firstName">Фильтр по имени (<paramref name="firstName"/>)</param>
        /// <param name="lastName">Фильтр по фамилии (<paramref name="lastName"/>)</param>
        /// <param name="dateOfBirth">Фильтр по дате рождения (<paramref name="dateOfBirth"/>)</param>
        /// <param name="pageNumber">Номер страницы (<paramref name="pageNumber"/>), по умолчанию 1</param>
        /// <param name="pageSize">Размер страницы (<paramref name="pageSize"/>), по умолчанию 10</param>
        /// <returns>JSON-объект с полями <c>totalCount</c> и <c>clients</c></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(string? firstName, string? lastName, DateOnly? dateOfBirth, int pageNumber = 1, int pageSize = 10)
        {
            var (clients, totalCount) = await _clientRepository.GetAllAsync(firstName, lastName, dateOfBirth, pageNumber, pageSize);
            return Ok(new { totalCount, clients });
        }

        /// <summary>
        /// Получить клиента по <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>Объект <see cref="Client"/> или статус 404, если не найден</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        /// <summary>
        /// Добавить нового клиента.
        /// </summary>
        /// <param name="client">Экземпляр <see cref="Client"/>, содержащий данные</param>
        /// <returns>Результат с данными созданного клиента</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Client client)
        {
            await _clientRepository.AddAsync(client);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }

        /// <summary>
        /// Обновить данные клиента по <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <param name="client">Новый объект <see cref="Client"/></param>
        /// <returns>204 No Content при успешном обновлении</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Client client)
        {
            if (id != client.Id)
                return BadRequest();

            await _clientRepository.UpdateAsync(client);
            return NoContent();
        }

        /// <summary>
        /// Удалить клиента по <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Идентификатор клиента</param>
        /// <returns>204 No Content при успешном удалении</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _clientRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
