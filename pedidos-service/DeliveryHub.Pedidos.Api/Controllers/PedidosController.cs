using DeliveryHub.Pedidos.Api.Dtos;
using DeliveryHub.Pedidos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryHub.Pedidos.Api.Controllers;

[ApiController] // Define que esta classe é um controlador de API
[Route("api/[controller]")] // Define a rota base para este controlador
public class PedidosController : ControllerBase //// Controlador base para APIs
{
    private readonly IPedidoService _pedidoService; // Serviço de pedidos injetado via construtor
    public PedidosController(IPedidoService pedidoService) // Construtor que recebe o serviço de pedidos
    {
        _pedidoService = pedidoService; // Atribui o serviço de pedidos ao campo privado
    }

    //Get api/pedidos
    [HttpGet] // Atributo que indica que este método responde a requisições GET
    public async Task<ActionResult<IEnumerable<PedidoResponse>>> GetTodos() // Método
    {
        var pedidos = await _pedidoService.ListarAsync(); // Chama o serviço para listar todos os pedidos
        return Ok(pedidos); // Retorna os pedidos com status 200 OK
    }

    //Get api/pedidos/{id}
    [HttpGet("{id:guid}")] // Atributo que indica que este método responde a requisições GET com um parâmetro GUID
    public async Task<ActionResult<PedidoResponse>> GetPorId(Guid id) // Método que recebe um ID de pedido
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id); // Chama o serviço para obter o pedido pelo ID
        if (pedido is null) return NotFound(); // Retorna 404 Not Found se o pedido não for encontrado
        return Ok(pedido); // Retorna o pedido com status 200 OK
    }

    //Post api/pedidos
    [HttpPost] // Atributo que indica que este método responde a requisições POST
    public async Task<ActionResult<PedidoResponse>> Criar([FromBody] CriarPedidoRequest request) // Método que recebe um pedido para criar
    {
        var criado = await _pedidoService.CriarAsync(request); // Chama o serviço para criar o pedido
        return CreatedAtAction(nameof(GetPorId), new { id = criado.Id }, criado); // Retorna 201 Created com a localização do novo recurso
    }

    // PUT api/pedidos/{id}/status
    [HttpPut("{id:guid}/status")]
    public async Task<ActionResult> AtualizarStatus(Guid id, [FromBody] AtualizarStatusPedidoRequest body)
    {
        var atualizado = await _pedidoService.AtualizarStatusAsync(id, body);

        if (!atualizado)
            return NotFound(new { mensagem = "Pedido não encontrado." });

        return NoContent();
    }
}
