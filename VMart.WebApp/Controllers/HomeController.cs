using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
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
        private readonly ICategoryService _categoryService;
        private readonly IOrderService _orderService;
        public HomeController(ILogger<HomeController> logger, IProductService productService, IAuthServices authServices, ICategoryService categoryService, IOrderService orderService)
        {
            _logger = logger;
            _productService = productService;
            _authServices = authServices;
            _categoryService = categoryService;
            _orderService = orderService;
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
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return Redirect("/Admin/Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return View(new LoginRequestDto { });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto,string ReturnUrl="/")
        {
            var res = await _authServices.Login(loginRequestDto);
            if (res.IsSuccess)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(7)
                };
                var userclaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,res.Result.Name??string.Empty),
                    new Claim(ClaimTypes.Email,res.Result.Email),
                    new Claim(ClaimTypes.Authentication,res.Result.Token),
                };
                Response.Cookies.Append("Name", res.Result.Name, cookieOptions);
                Response.Cookies.Append("Email", res.Result.Email, cookieOptions);
                Response.Cookies.Append("Token", res.Result.Token, cookieOptions);
                foreach (var item in res.Result.Role)
                {
                    Response.Cookies.Append("Role", item, cookieOptions);
                    userclaims.Add(new Claim(ClaimTypes.Role, item));
                }

                var identity = new ClaimsIdentity(userclaims, CookieAuthenticationDefaults.AuthenticationScheme);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddYears(1)
                };
                await HttpContext.SignInAsync(new ClaimsPrincipal(identity), properties);
                if (res.Result.Role.Any(x => x.Equals("Admin")))
                {
                    return Redirect("/Admin/Index");
                }
                else
                {
                    if (ReturnUrl == "/")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Redirect(ReturnUrl);
                    }
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
        [HttpPost]
        public async Task<IActionResult> GetCategory()
        {
            var res = await _categoryService.GetAsync();
            return Json(res);
        }
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Cart()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderDto orderDto)
        {
            var res = await _orderService.Place(orderDto);
            return Json(res);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("home/product/{Id}")]
        public async Task<IActionResult> product(int Id)
        {
            var res = await _productService.GetByCategoryIdAsync(Id);
            return View(res);
        }
        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = "/Home/Login")
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            HttpContext.Response.Cookies.Delete("Token");
            HttpContext.Response.Cookies.Delete("Name");
            HttpContext.Response.Cookies.Delete("Email");
            return LocalRedirect(returnUrl);
        }
    }
}
