using Microsoft.AspNetCore.Identity;

namespace VMart.AuthAPI.Models
{
    public class ApplicationUser:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
