using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VMart.Services.ProductAPI.Data;
using VMart.Services.ProductAPI.Models;
using VMart.Services.ProductAPI.Models.Dto;
using VMart.Services.ProductAPI.Services.IServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VMart.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;
        public ProductController(AppDbContext appDbContext, IMapper mapper, IFileUploadService fileUploadService)
        {
            _appDbContext = appDbContext;
            _response = new ResponseDto { Message = "Sorry,Something went wrong try aftersome." };
            _mapper = mapper;
            _fileUploadService = fileUploadService;
        }
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                var res = _appDbContext.Products.ToList();
                _response.IsSuccess = true;
                _response.Message = "Request Completed Successfully.";
                _response.Result = res;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpGet("Id")]
        public async Task<object> Get(int Id)
        {
            try
            {
                var res = _appDbContext.Products.Where(x => x.Id == Id).FirstOrDefault();
                _response.IsSuccess = true;
                _response.Message = "Request Completed Successfully.";
                _response.Result = res;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPost]
        public async Task<object> Add([FromForm]ProductDto productDto)
        {
            try
            {
                var data = _mapper.Map<Products>(productDto);
                if (productDto.ProductImage != null)
                {
                    var fileRes = _fileUploadService.Upload(new FileUploadDto
                    {
                        file = productDto.ProductImage,
                        FileName = DateTime.Now.ToString("ddMMyyyyhhmmss"),
                       FilePath = "wwwroot/upload/productimage/" 
                    });
                    if (!fileRes.IsSuccess)
                    {
                        _response.Message = fileRes.Message;
                        return _response;
                    }
                    data.ProductImage = fileRes.Result.ToString();
                }                
                var res = await _appDbContext.Products.AddAsync(data);
                await _appDbContext.SaveChangesAsync();
                _response.IsSuccess = true;
                _response.Message = "Request Completed Successfully.";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpPut]
        public async Task<object> Update([FromForm]ProductDto productDto)
        {
            try
            {
                var res = await _appDbContext.Products.FirstOrDefaultAsync(c => c.Id == productDto.Id);
                if (productDto.ProductImage != null)
                {
                    var fileRes = _fileUploadService.Upload(new FileUploadDto
                    {
                        file = productDto.ProductImage,
                        FileName = DateTime.Now.ToString("ddMMyyyyhhmmss"),
                        FilePath = "wwwroot/upload/productimage/"
                    });
                    if (!fileRes.IsSuccess)
                    {
                        _response.Message = fileRes.Message;
                        return _response;
                    }
                    res.ProductImage = fileRes.Result.ToString();
                }
                res.Product_Name = productDto.Product_Name;
                res.Description = productDto.Description;
                res.CategoryId = productDto.CategoryId;
                await _appDbContext.SaveChangesAsync();
                _response.IsSuccess = true;
                _response.Message = "Request Completed Successfully.";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }
        [HttpDelete("Id")]
        public async Task<object> Delete(int Id)
        {
            try
            {
                var res = await _appDbContext.Products.FirstOrDefaultAsync(c => c.Id == Id);
                _appDbContext.Products.Remove(res);
                await _appDbContext.SaveChangesAsync();
                _response.IsSuccess = true;
                _response.Message = "Request Completed Successfully.";
                _response.Result = res;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
