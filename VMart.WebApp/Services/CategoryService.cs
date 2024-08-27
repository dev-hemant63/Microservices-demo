using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;
using VMart.WebApp.Services.IServices;
using VMart.WebApp.Utility;

namespace VMart.WebApp.Services
{
    public class CategoryService: ICategoryService
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
                Url = "https://localhost:7083/gateway/api/category",
                RequestType = RequestType.GET,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
    }
}
