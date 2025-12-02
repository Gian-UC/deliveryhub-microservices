using DeliveryHub.Entregas.Api.Models;

namespace DeliveryHub.Entregas.Api.Repositories
{
    public interface IEntregaRepository
    {
        Task AdicionarAsync(Entrega entrega);
        Task<IEnumerable<Entrega>> ListarAsync();
        Task<Entrega?> ObterPorIdAsync(Guid id);
    }
}