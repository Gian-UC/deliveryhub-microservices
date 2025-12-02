using Microsoft.EntityFrameworkCore;
using DeliveryHub.Pedidos.Api.Models;

namespace DeliveryHub.Pedidos.Api.Data
{
    public class PedidosDbContext : DbContext
    {
        public PedidosDbContext(DbContextOptions<PedidosDbContext> options)
            : base(options) {}

        public DbSet<Pedido> Pedidos => Set<Pedido>();
    }
}