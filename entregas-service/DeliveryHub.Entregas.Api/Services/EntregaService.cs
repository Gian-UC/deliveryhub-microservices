using DeliveryHub.Entregas.Api.Dtos;
using DeliveryHub.Entregas.Api.Models;
using DeliveryHub.Entregas.Api.Repositories;

namespace DeliveryHub.Entregas.Api.Services
{
    public class EntregaService : IEntregaService
    {
        private readonly IEntregaRepository _repository;

        public EntregaService(IEntregaRepository repository)
        {
            _repository = repository;
        }

        public async Task<EntregaResponse> CriarEntregaAsync(Guid pedidoId, string clienteNome)
        {
            var entrega = new Entrega
            {
                PedidoId = pedidoId,
                ClienteNome = clienteNome,
                Status = StatusEntrega.EmRota
            };

            await _repository.AdicionarAsync(entrega);

            return Map(entrega);
        }

        public async Task<IEnumerable<EntregaResponse>> ListarAsync()
        {
            var entregas = await _repository.ListarAsync();
            return entregas.Select(Map);
        }

        public async Task<EntregaResponse?> ObterPorIdAsync(Guid id)
        {
            var entrega = await _repository.ObterPorIdAsync(id);
            return entrega is null ? null : Map(entrega);
        }

        private static EntregaResponse Map(Entrega entrega)
        {
            return new EntregaResponse
            {
                Id = entrega.Id,
                PedidoId = entrega.PedidoId,
                ClienteNome = entrega.ClienteNome,
                Status = entrega.Status.ToString(),
                CriadoEm = entrega.CriadoEm
            };
        }
    }
}
