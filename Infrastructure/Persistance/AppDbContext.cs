using Domain.Aggregate.Cart;
using Domain.Aggregate.Order;
using Domain.Aggregate.Product;
using Domain.Aggregate.User;
using Infrastructure.Configuration;
using Infrastructure.Message;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistance
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<Cart> Carts => Set<Cart>();

        public DbSet<OutboxMessage> Messages => Set<OutboxMessage>();

        public DbSet<User> Users => Set<User>();

        public AppDbContext(DbContextOptions options) : base(options) {
        
        }

        public async Task<int> SaveChangesAsync(CancellationToken cts = default) => await base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().Property(p => p.Version).IsRowVersion();
            modelBuilder.Entity<Order>().OwnsMany(o => o.Items);
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
