using VMart.WebApp.Models.Dto;

namespace VMart.WebApp.Services.IServices
{
    public interface IAuthServices
    {
        Task<ResponseDto<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto<object>> Register(RegisterRequestDto registerRequestDto);
    }
}
