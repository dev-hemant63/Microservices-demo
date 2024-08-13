using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VMart.AuthAPI.Models;

namespace VMart.AuthAPI.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser, ApplicationRole,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
    }
}
