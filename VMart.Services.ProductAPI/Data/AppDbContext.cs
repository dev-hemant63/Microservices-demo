using Microsoft.EntityFrameworkCore;
using VMart.Services.ProductAPI.Models;

namespace VMart.Services.ProductAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }
    }
}
