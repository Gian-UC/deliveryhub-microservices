using DeliveryHub.Entregas.Api.Models;

namespace DeliveryHub.Entregas.Api.Repositories

{
    public interface IEntregaRepository
    {
        Task<List<Entrega>> ListarAsync();
        Task<Entrega?> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Entrega entrega);
    }
}