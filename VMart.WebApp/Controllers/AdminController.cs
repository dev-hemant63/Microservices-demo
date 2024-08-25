using Microsoft.AspNetCore.Mvc;
using VMart.WebApp.Services.IServices;

namespace VMart.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        public AdminController(IProductService productService)
        {
            _productService = productService;
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
            return PartialView();
        }
    }
}
