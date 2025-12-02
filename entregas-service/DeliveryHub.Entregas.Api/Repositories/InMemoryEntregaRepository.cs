using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryHub.Entregas.Api.Models;

namespace DeliveryHub.Entregas.Api.Repositories
{
    public class InMemoryEntregaRepository : IEntregaRepository
    {
        private readonly List<Entrega> _entregas = new();

        public Task AdicionarAsync(Entrega entrega)
        {
            _entregas.Add(entrega);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Entrega>> ListarAsync()
        {
            // Converte para IEnumerable para bater com a interface
            return Task.FromResult(_entregas.AsEnumerable());
        }

        public Task<Entrega?> ObterPorIdAsync(Guid id)
        {
            var entrega = _entregas.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(entrega);
        }
    }
}
