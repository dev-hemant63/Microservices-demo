using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;
using VMart.WebApp.Services.IServices;
using VMart.WebApp.Utility;

namespace VMart.WebApp.Services
{
    public class AuthServices: IAuthServices
    {
        private readonly IRequestBase _requestBase;
        private readonly ITokenProvider _tokenProvider;
        public AuthServices(IRequestBase requestBase, ITokenProvider tokenProvider)
        {
            _requestBase = requestBase;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseDto<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
        {
            var res = await _requestBase.SendAsync<LoginResponseDto>(new RequestDto
            {
                Url = "http://localhost:5250/gateway/api/account/login",
                RequestType = RequestType.POST,
                RequestBody = loginRequestDto
            });
            
            return res;
        }
        public async Task<ResponseDto<object>> Register(RegisterRequestDto registerRequestDto)
        {
            var res = await _requestBase.SendAsync<object>(new RequestDto
            {
                Url = "http://localhost:5250/gateway/api/account/create",
                RequestType = RequestType.POST,
                RequestBody = registerRequestDto
            });

            return res;
        }
        public async Task<ResponseDto<List<AppUser>>> GetAppUsers()
        {
            var res = await _requestBase.SendAsync<List<AppUser>>(new RequestDto
            {
                Url = "http://localhost:5250/gateway/api/account/GetList",
                RequestType = RequestType.GET,
                Token = await _tokenProvider.GetToken()
            });

            return res;
        }
    }
}
