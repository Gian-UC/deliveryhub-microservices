using DeliveryHub.Pedidos.Api.Data;
using DeliveryHub.Pedidos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryHub.Pedidos.Api.Repositories
{
    public class EfPedidoRepository : IPedidoRepository 
    {
        private readonly PedidosDbContext _db;

        public EfPedidoRepository(PedidosDbContext db)
        {
            _db = db;
        }

        public async Task<Pedido> AdicionarAsync(Pedido pedido)
        {
            _db.Pedidos.Add(pedido);
            await _db.SaveChangesAsync();
            return pedido;
        }

        public async Task<IReadOnlyList<Pedido>> ListarAsync()
        {
            return await _db.Pedidos.ToListAsync();
        }

        public async Task<Pedido?> ObterPorIdAsync(Guid id)
        {
            return await _db.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            _db.Pedidos.Update(pedido);
            await _db.SaveChangesAsync();
        }
    }
}
