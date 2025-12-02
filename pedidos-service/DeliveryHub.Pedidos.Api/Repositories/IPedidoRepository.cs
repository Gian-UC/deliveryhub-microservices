using DeliveryHub.Pedidos.Api.Models;

namespace DeliveryHub.Pedidos.Api.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido> AdicionarAsync(Pedido pedido);
        Task<IReadOnlyList<Pedido>> ListarAsync();
        Task<Pedido?> ObterPorIdAsync(Guid id);
        Task AtualizarAsync(Pedido pedido);
    }
}
