using VMart.WebApp.Services.IServices;

namespace VMart.WebApp.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> GetToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["Token"];
            return token;
        }
    }
}
