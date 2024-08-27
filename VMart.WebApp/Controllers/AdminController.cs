using Microsoft.AspNetCore.Mvc;
using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;
using VMart.WebApp.Services.IServices;

namespace VMart.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Product()
        {
            var res = await _productService.GetAsync();
            return View(res);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int Id)
        {
            var model = new ProductAddOrEditVM();
            if (Id != 0)
            {
                var res = await _productService.GetByIdAsync(Id);
                if (res.IsSuccess)
                {
                    model.Products = res.Result;
                }
            }
            var catRes = await _categoryService.GetAsync();
            if (catRes.IsSuccess)
            {
                model.ProductCategory = catRes.Result;
            }
            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveProduct(AddProductDto addProductDto)
        {
            var res = await _productService.AddAsync(addProductDto);
            return Json(res);
        }
    }
}
