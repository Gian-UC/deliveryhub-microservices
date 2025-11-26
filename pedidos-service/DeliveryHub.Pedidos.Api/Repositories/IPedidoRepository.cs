using DeliveryHub.Pedidos.Api.Models;

namespace DeliveryHub.Pedidos.Api.Repositories;

public interface IPedidoRepository // Interface para o repositório de pedidos
{
    Task<Pedido> AdicionarAsync(Pedido pedido); // Método para adicionar um novo pedido
    Task<Pedido?> ObterPorIdAsync(Guid id); // Método para obter um pedido por ID
    Task<IReadOnlyList<Pedido>> ListarAsync(); // Método para listar todos os pedidos
    Task AtualizarAsync(Pedido pedido); // Método para atualizar um pedido existente
}
