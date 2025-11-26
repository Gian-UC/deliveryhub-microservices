using DeliveryHub.Pedidos.Api.Dtos;

namespace DeliveryHub.Pedidos.Api.Services
{
    public interface IPedidoService // Interface para o serviço de pedidos
    {
        Task<PedidoResponse> CriarAsync(CriarPedidoRequest request); // Método para criar um novo pedido
        Task<IReadOnlyList<PedidoResponse>> ListarAsync(); // Método para listar todos os pedidos
        Task<PedidoResponse?> ObterPorIdAsync(Guid id); // Método para obter um pedido por ID
        Task<bool> AtualizarStatusAsync(Guid id, AtualizarStatusPedidoRequest request); // Método para atualizar o status de um pedido
    }
}