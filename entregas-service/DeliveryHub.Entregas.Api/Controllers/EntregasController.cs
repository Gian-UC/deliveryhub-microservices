using DeliveryHub.Entregas.Api.Models;
using DeliveryHub.Entregas.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryHub.Entregas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntregasController : ControllerBase
    {
        private readonly IEntregaRepository _repository;

        public EntregasController(IEntregaRepository repository)
        {
            _repository = repository;
        }

        // GET /api/entregas
        [HttpGet]
        public async Task<IActionResult> ListarAsync()
        {
            var entregas = await _repository.ListarAsync();
            return Ok(entregas);
        }

        // GET /api/entregas/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorIdAsync(Guid id)
        {
            var entrega = await _repository.ObterPorIdAsync(id);

            if (entrega is null)
                return NotFound();

            return Ok(entrega);
        }
    }
}
