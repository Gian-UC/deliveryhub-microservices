using DeliveryHub.Pedidos.Api.Models;

namespace DeliveryHub.Pedidos.Api.Repositories;

public class InMemoryPedidoRepository : IPedidoRepository
{
    private readonly List<Pedido> _pedidos = new(); // Lista em memória para armazenar os pedidos

    public Task<Pedido> AdicionarAsync(Pedido pedido) // Método para adicionar um novo pedido
    {
        _pedidos.Add(pedido); // Adiciona o pedido à lista
        return Task.FromResult(pedido); // Retorna o pedido adicionado
    }

    public Task<Pedido?> ObterPorIdAsync(Guid id) // Método para obter um pedido por ID
    {
        var pedido = _pedidos.FirstOrDefault(p => p.Id == id); // Busca o pedido na lista
        return Task.FromResult(pedido); // Retorna o pedido encontrado ou null
    }

    public Task<IReadOnlyList<Pedido>> ListarAsync() // Método para listar todos os pedidos
    {
        return Task.FromResult((IReadOnlyList<Pedido>)_pedidos); // Retorna a lista de pedidos
    }

    public Task AtualizarAsync(Pedido pedido) // Método para atualizar um pedido existente
    {
        var idx = _pedidos.FindIndex(p => p.Id == pedido.Id);
        if (idx >= 0) _pedidos[idx] = pedido;
        return Task.CompletedTask;
    }
}
