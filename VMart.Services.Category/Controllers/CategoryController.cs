using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VMart.Services.Category.Data;
using VMart.Services.Category.Models;
using VMart.Services.Category.Models.Dto;

namespace VMart.Services.Category.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ResponseDto _response;
        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _response = new ResponseDto { Message = "Sorry,Something went wrong try aftersome." };
        }
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                var res = _appDbContext.Category.ToList();
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
        [HttpGet("{Id}")]
        public async Task<object> Get(int Id)
        {
            try
            {
                var res = _appDbContext.Category.Where(x=>x.Id == Id).FirstOrDefault();
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
        public async Task<object> Add(ProductCategory categoryDto)
        {
            try
            {
                var res = await _appDbContext.Category.AddAsync(categoryDto);
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
        public async Task<object> Update(ProductCategory categoryDto)
        {
            try
            {
                var res = await _appDbContext.Category.FirstOrDefaultAsync(c=>c.Id == categoryDto.Id);
                res.Name = categoryDto.Name;
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
        [HttpDelete("{Id}")]
        public async Task<object> Delete(int Id)
        {
            try
            {
                var res = await _appDbContext.Category.FirstOrDefaultAsync(c => c.Id == Id);
                _appDbContext.Category.Remove(res);
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
