using DeliveryHub.Entregas.Api.Data;
using DeliveryHub.Entregas.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryHub.Entregas.Api.Repositories
{
    public class EntregaRepository : IEntregaRepository
    {
        private readonly EntregasDbContext _context;

        public EntregaRepository(EntregasDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Entrega entrega)
        {
            await _context.Entregas.AddAsync(entrega);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entrega>> ListarAsync()
        {
            return await _context.Entregas.ToListAsync();
        }

        public async Task<Entrega?> ObterPorIdAsync(Guid id)
        {
            return await _context.Entregas.FindAsync(id);
        }
    }
}