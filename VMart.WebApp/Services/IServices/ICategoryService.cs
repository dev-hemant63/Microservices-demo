using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;

namespace VMart.WebApp.Services.IServices
{
    public interface ICategoryService
    {
        Task<ResponseDto<IEnumerable<ProductCategory>>> GetAsync();
    }
}
