using System.Net.Http.Headers;
using System.Text;
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
                Url = "http://localhost:5250/gateway/api/Product",
                RequestType = RequestType.GET,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
        public async Task<ResponseDto<Products>> GetByIdAsync(int Id)
        {
            var res = await _requestBase.SendAsync<Products>(new RequestDto
            {
                Url = $"http://localhost:5250/gateway/api/Product/{Id}",
                RequestType = RequestType.GET,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
        public async Task<ResponseDto<List<Products>>> GetByCategoryIdAsync(int categoryId)
        {
            var res = await _requestBase.SendAsync<List<Products>>(new RequestDto
            {
                Url = $"http://localhost:5250/gateway/api/Product/category/{categoryId}",
                RequestType = RequestType.GET,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
        public async Task<ResponseDto<Products>> DeleteAsync(int Id)
        {
            var res = await _requestBase.SendAsync<Products>(new RequestDto
            {
                Url = $"http://localhost:5250/gateway/api/Product/{Id}",
                RequestType = RequestType.DELETE,
                Token = await _tokenProvider.GetToken()
            });
            return res;
        }
        public async Task<ResponseDto<object>> AddAsync(AddProductDto addProductDto)
        {
            var multipartContent = new MultipartFormDataContent();

            multipartContent.Add(new StringContent(addProductDto.Id.ToString()), nameof(addProductDto.Id));
            multipartContent.Add(new StringContent(addProductDto.CategoryId.ToString()), nameof(addProductDto.CategoryId));
            multipartContent.Add(new StringContent(addProductDto.Product_Name.ToString()), nameof(addProductDto.Product_Name));
            multipartContent.Add(new StringContent(addProductDto.Price.ToString()), nameof(addProductDto.Price));
            multipartContent.Add(new StringContent(addProductDto.Description.ToString()), nameof(addProductDto.Description));

            if (addProductDto.ProductImage != null)
            {
                using (var stream = addProductDto.ProductImage.OpenReadStream())
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        var fileContent = new ByteArrayContent(reader.ReadBytes((int)stream.Length));
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream"); // or the actual MIME type of the file
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "\"ProductImage\"",
                            FileName = $"\"{addProductDto.ProductImage.FileName}\""
                        };

                        multipartContent.Add(fileContent, "ProductImage");
                    }
                }
            }
            var res = await _requestBase.SendAsync<object>(new RequestDto
            {
                Url = "http://localhost:5250/gateway/api/Product",
                RequestType = addProductDto.Id==0?RequestType.POST:RequestType.PUT,
                Token = await _tokenProvider.GetToken(),
                RequestBody = multipartContent
            });
            return res;
        }
    }
}
