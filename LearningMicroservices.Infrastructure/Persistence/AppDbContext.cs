using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using OrderManagement.Domain.Entities;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<OrderItems> OrderItems => Set<OrderItems>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Order>(builder =>
            {
                builder.HasKey(o => o.Id);

                // 📦 Configure Value Object: Address (owned by Order)
                builder.OwnsOne(o => o.ShippingAddress, address =>
                {
                    address.Property(a => a.Street).HasColumnName("Street");
                    address.Property(a => a.City).HasColumnName("City");
                    address.Property(a => a.Country).HasColumnName("Country");
                });

                // 📦 Configure Collection of OrderItems
                builder.OwnsMany(o => o.Items, item =>
                {
                    item.WithOwner().HasForeignKey("OrderId");
                    item.Property<Guid>("Id"); // shadow key
                    item.HasKey("Id");

                    // 🧩 Configure Value Object: Money (owned by OrderItem)
                    item.OwnsOne(i => i.UnitPrice, money =>
                    {
                        money.Property(m => m.Amount)
                             .HasColumnName("UnitPrice_Amount");
                        money.Property(m => m.Currency)
                             .HasColumnName("UnitPrice_Currency");
                    });
                });
            });
        }
    }
}
