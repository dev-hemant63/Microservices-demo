using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;

namespace VMart.WebApp.Services.IServices
{
    public interface IProductService
    {
        Task<ResponseDto<List<Products>>> GetAsync();
    }
}
