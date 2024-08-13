using System.Security.Claims;

namespace VMart.AuthAPI.Repository.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateTokenAsync(IEnumerable<Claim> claims);
        Task<bool> ValidateTokenAsync(string token);
    }
}
