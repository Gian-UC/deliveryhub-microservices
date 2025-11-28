using DeliveryHub.Entregas.Api.Models;

namespace DeliveryHub.Entregas.Api.Repositories

{
    public class InMemoryEntregaRepository : IEntregaRepository 
    {
        private readonly List<Entrega> _entregas = new(); 
        public Task<List<Entrega>> ListarAsync()
        {
            return Task.FromResult(_entregas);
        }

        public Task<Entrega?> ObterPorIdAsync(Guid id)
        {
            var entrega = _entregas.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(entrega);
        }

        public Task AdicionarAsync(Entrega entrega)
        {
            _entregas.Add(entrega);
            return Task.CompletedTask;
        }
    }
}