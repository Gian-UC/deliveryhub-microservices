using DeliveryHub.Entregas.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryHub.Entregas.Api.Data
{
    public class EntregasDbContext : DbContext
    {
        public EntregasDbContext(DbContextOptions<EntregasDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Entrega> Entregas { get; set; }
    }
}