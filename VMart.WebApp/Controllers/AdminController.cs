using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;
using VMart.WebApp.Services.IServices;

namespace VMart.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IOrderService _orderService;
        private readonly IAuthServices _authServices;
        public AdminController(IProductService productService, ICategoryService categoryService, IOrderService orderService, IAuthServices authServices)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
            _authServices = authServices;
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
            var model = new ProductAddOrEditVM
            {
                Products = new Products()
            };
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
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var res = await _productService.DeleteAsync(Id);
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> Category()
        {
            var catRes = await _categoryService.GetAsync();
            return View(catRes);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEditCat(int Id)
        {
            var category = new ProductCategory();
            if (Id != 0)
            {
                var catRes = await _categoryService.GetByIdAsync(Id);
                if (catRes.IsSuccess)
                {
                    category = catRes.Result;
                }
            }
            return PartialView(category);
        }
        [HttpPost]
        public async Task<IActionResult> SaveCategory(string Name, int Id)
        {
            var catRes = await _categoryService.SaveAsync(new CategoryDto
            {
                Name = Name,
                Id = Id
            });
            return Json(catRes);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            var res = await _categoryService.DeleteAsync(Id);
            return Json(res);
        }
        [HttpGet]
        public async Task<IActionResult> Order()
        {
            var list = await _orderService.GetAsync();
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var list = await _orderService.GetDetailsAsync(orderId);
            return PartialView(list);
        }
        [HttpGet]
        public async Task<IActionResult> Customer()
        {
            var list = await _authServices.GetAppUsers();
            return View(list);
        }
    }
}
