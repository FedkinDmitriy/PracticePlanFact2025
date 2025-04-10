using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с клиентами
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
        /// Получить список клиентов с возможностью фильтрации и пагинации
        /// </summary>
        /// <param name="firstName">Фильтр по имени</param>
        /// <param name="lastName">Фильтр по фамилии</param>
        /// <param name="dateOfBirth">Фильтр по дате рождения</param>
        /// <param name="pageNumber">Номер страницы (по умолчанию 1)</param>
        /// <param name="pageSize">Размер страницы (по умолчанию 10)</param>
        /// <returns>Список клиентов и общее количество</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(string? firstName, string? lastName, DateOnly? dateOfBirth, int pageNumber = 1, int pageSize = 10)
        {
            var (clients, totalCount) = await _clientRepository.GetAllAsync(firstName, lastName, dateOfBirth, pageNumber, pageSize);
            return Ok(new { totalCount, clients });
        }

        /// <summary>
        /// Получить клиента по ID
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <returns>Клиент с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        /// <summary>
        /// Добавить нового клиента
        /// </summary>
        /// <param name="client">Данные клиента</param>
        /// <returns>Созданный клиент</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Client client)
        {
            await _clientRepository.AddAsync(client);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }

        /// <summary>
        /// Обновить существующего клиента
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <param name="client">Новые данные клиента</param>
        /// <returns>Результат выполнения</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Client client)
        {
            if (id != client.Id)
                return BadRequest();

            await _clientRepository.UpdateAsync(client);
            return NoContent();
        }

        /// <summary>
        /// Удалить клиента по ID
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _clientRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
