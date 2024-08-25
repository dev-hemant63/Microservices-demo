using VMart.Services.EmailAPI.Models.DTOs;

namespace VMart.Services.EmailAPI.Services.IService
{
    public interface IEmailProvider
    {
        Task<ResponseDto> send(SendDto sendDto);
    }
}
