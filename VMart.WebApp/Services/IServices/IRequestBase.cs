using VMart.WebApp.Models.Dto;

namespace VMart.WebApp.Services.IServices
{
    public interface IRequestBase
    {
        Task<ResponseDto<T>> SendAsync<T>(RequestDto request);
    }
}
