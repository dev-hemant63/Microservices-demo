using Microsoft.EntityFrameworkCore;
using VMart.Services.Order.Models;

namespace VMart.Services.Order.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>()
                .HasOne(e => e.Order)
                .WithMany()
                .HasForeignKey(e => e.OrderId);
        }
    }
}
