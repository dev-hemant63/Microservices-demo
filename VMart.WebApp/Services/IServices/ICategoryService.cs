using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;

namespace VMart.WebApp.Services.IServices
{
    public interface ICategoryService
    {
        Task<ResponseDto<IEnumerable<ProductCategory>>> GetAsync();
        Task<ResponseDto<object>> SaveAsync(CategoryDto categoryDto);
        Task<ResponseDto<ProductCategory>> GetByIdAsync(int Id);
        Task<ResponseDto<object>> DeleteAsync(int Id);
    }
}
