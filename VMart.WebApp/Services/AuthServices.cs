using VMart.WebApp.Models.Dto;
using VMart.WebApp.Services.IServices;
using VMart.WebApp.Utility;

namespace VMart.WebApp.Services
{
    public class AuthServices: IAuthServices
    {
        private readonly IRequestBase _requestBase;
        public AuthServices(IRequestBase requestBase)
        {
            _requestBase = requestBase;
        }
        public async Task<ResponseDto<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
        {
            var res = await _requestBase.SendAsync<LoginResponseDto>(new RequestDto
            {
                Url = "https://localhost:7039/api/account/login",
                RequestType = RequestType.POST,
                RequestBody = loginRequestDto
            });
            
            return res;
        }
    }
}
