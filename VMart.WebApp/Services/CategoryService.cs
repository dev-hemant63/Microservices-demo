using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;
using VMart.WebApp.Services.IServices;
using VMart.WebApp.Utility;

namespace VMart.WebApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRequestBase _requestBase;
        private readonly ITokenProvider _tokenProvider;
        public CategoryService(IRequestBase requestBase, ITokenProvider tokenProvider)
        {
            _requestBase = requestBase;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseDto<IEnumerable<ProductCategory>>> GetAsync()
        {
            var res = await _requestBase.SendAsync<IEnumerable<ProductCategory>>(new RequestDto
            {
                Url = "http://localhost:5250/gateway/api/category",
                RequestType = RequestType.GET,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
        public async Task<ResponseDto<object>> SaveAsync(CategoryDto categoryDto)
        {
            var res = await _requestBase.SendAsync<object>(new RequestDto
            {
                Url = "http://localhost:5250/gateway/api/category",
                RequestType = categoryDto.Id == 0 ? RequestType.POST : RequestType.PUT,
                Token = await _tokenProvider.GetToken(),
                RequestBody = categoryDto
            });
            return res;
        }
        public async Task<ResponseDto<ProductCategory>> GetByIdAsync(int Id)
        {
            var res = await _requestBase.SendAsync<ProductCategory>(new RequestDto
            {
                Url = $"http://localhost:5250/gateway/api/category/{Id}",
                RequestType = RequestType.GET,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
        public async Task<ResponseDto<object>> DeleteAsync(int Id)
        {
            var res = await _requestBase.SendAsync<object>(new RequestDto
            {
                Url = $"http://localhost:5250/gateway/api/category/{Id}",
                RequestType = RequestType.DELETE,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
    }
}
