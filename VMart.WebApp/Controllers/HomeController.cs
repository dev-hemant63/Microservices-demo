using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VMart.WebApp.Models;
using VMart.WebApp.Models.Dto;
using VMart.WebApp.Services.IServices;
using VMart.WebApp.Utility;

namespace VMart.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IAuthServices _authServices;
        public HomeController(ILogger<HomeController> logger, IProductService productService, IAuthServices authServices)
        {
            _logger = logger;
            _productService = productService;
            _authServices = authServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> GetProduct()
        {
            var res = await _productService.GetAsync();
            return Json(res);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginRequestDto { });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var res = await _authServices.Login(loginRequestDto);
            if (res.IsSuccess)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(7)
                };
                Response.Cookies.Append("Name", res.Result.Name, cookieOptions);
                Response.Cookies.Append("Email", res.Result.Email, cookieOptions);
                Response.Cookies.Append("Token", res.Result.Token, cookieOptions);
                foreach (var item in res.Result.Role)
                {
                    Response.Cookies.Append("Role", item, cookieOptions);
                }
                if (res.Result.Role.Any(x => x.Equals("Admin")))
                {
                    return Redirect("/Admin/Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Message = res.Message;
            ViewBag.Code = res.IsSuccess == true ? 200 : 500;
            return RedirectToAction("Login");
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View(new RegisterRequestDto());
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            var res = await _authServices.Register(registerRequestDto);
            if (res.IsSuccess)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View(registerRequestDto);
            }
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
