using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;
using VMart.WebApp.Services.IServices;
using VMart.WebApp.Utility;

namespace VMart.WebApp.Services
{
    public class ProductService: IProductService
    {
        private readonly IRequestBase _requestBase;
        private readonly ITokenProvider _tokenProvider;
        public ProductService(IRequestBase requestBase, ITokenProvider tokenProvider)
        {
            _requestBase = requestBase;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseDto<List<Products>>> GetAsync()
        {
            var res = await _requestBase.SendAsync<List<Products>>(new RequestDto
            {
                Url = "https://localhost:7127/api/Product",
                RequestType = RequestType.GET,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
    }
}
