using DeliveryHub.Entregas.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryHub.Entregas.Api.Controllers
{
    [ApiController]
    [Route("api/entregas")]
    public class EntregasController : ControllerBase
    {
        private readonly IEntregaService _service;

        public EntregasController(IEntregaService service)
        {
            _service = service;
        }

        // GET api/entregas
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var entregas = await _service.ListarAsync();
            return Ok(entregas);
        }

        // GET api/entregas/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var entrega = await _service.ObterPorIdAsync(id);

            if (entrega == null)
                return NotFound(new { message = "Entrega não encontrada" });

            return Ok(entrega);
        }
    }
}
