using DeliveryHub.Entregas.Api.Models;
using DeliveryHub.Entregas.Api.Repositories;

namespace DeliveryHub.Entregas.Api.Services
{
    public interface IEntregaService
    {
        Task CriarEntregaPorEventoAsync(Guid pedidoId, string clienteNome, decimal valorTotal);
        Task<List<Entrega>> ListarAsync();
        Task<Entrega?> ObterPorIdAsync(Guid id);
    }

    public class EntregaService : IEntregaService
    {
        private readonly IEntregaRepository _repository;

        public EntregaService(IEntregaRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarEntregaPorEventoAsync(Guid pedidoId, string clienteNome, decimal valorTotal)
        {
            var entrega = new Entrega
            {
                Id = Guid.NewGuid(),
                PedidoId = pedidoId,
                ClienteNome = clienteNome,
                ValorTotal = valorTotal,
                Status = "Em Rota",
                CriadoEm = DateTime.UtcNow
            };

            await _repository.AdicionarAsync(entrega);

            Console.WriteLine($"ENTREGA CRIADA PARA O PEDIDO {pedidoId}!");
        }

        public Task<List<Entrega>> ListarAsync()
        {
            return _repository.ListarAsync();
        }

        public Task<Entrega?> ObterPorIdAsync(Guid id)
        {
            return _repository.ObterPorIdAsync(id);
        }
    }
}
