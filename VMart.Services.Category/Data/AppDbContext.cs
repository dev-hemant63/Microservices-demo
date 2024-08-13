using Microsoft.EntityFrameworkCore;
using VMart.Services.Category.Models;

namespace VMart.Services.Category.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ProductCategory> Category { get; set; }
    }
}
