using DeliveryHub.Entregas.Api.Dtos;

namespace DeliveryHub.Entregas.Api.Services
{
    public interface IEntregaService
    {
        Task<EntregaResponse> CriarEntregaAsync(Guid pedidoId, string clienteNome);
        Task<IEnumerable<EntregaResponse>> ListarAsync();
        Task<EntregaResponse?> ObterPorIdAsync(Guid id);
    }
}