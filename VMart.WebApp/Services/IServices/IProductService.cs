using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;

namespace VMart.WebApp.Services.IServices
{
    public interface IProductService
    {
        Task<ResponseDto<List<Products>>> GetAsync();
        Task<ResponseDto<object>> AddAsync(AddProductDto addProductDto);
        Task<ResponseDto<Products>> GetByIdAsync(int Id);
        Task<ResponseDto<Products>> DeleteAsync(int Id);
        Task<ResponseDto<List<Products>>> GetByCategoryIdAsync(int categoryId);
    }
}
