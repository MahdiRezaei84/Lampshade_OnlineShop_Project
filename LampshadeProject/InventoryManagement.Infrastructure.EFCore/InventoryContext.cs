using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.EFCore
{
    public class InventoryContext : DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(Inventory).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
