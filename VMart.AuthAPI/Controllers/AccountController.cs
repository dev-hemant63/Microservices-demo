using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VMart.AuthAPI.Models;
using VMart.AuthAPI.Models.Dto;
using VMart.AuthAPI.Repository.Interfaces;

namespace VMart.AuthAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, RoleManager<ApplicationRole> roleManager, ITokenGenerator tokenGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _responseDto = new ResponseDto { Message = "Sorry,Something went wrong try after sometime.", Result = null };
            _mapper = mapper;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost(nameof(create))]
        public async Task<IActionResult> create(UserRegistrationReq registrationReq)
        {
            try
            {
                var userRes = await _userManager.FindByEmailAsync(registrationReq.Email);
                if (userRes == null)
                {
                    var user = _mapper.Map<ApplicationUser>(registrationReq);
                    user.UserName = registrationReq.Email;
                    var res = await _userManager.CreateAsync(user, registrationReq.Password);
                    if (res.Succeeded)
                    {
                        _responseDto.IsSuccess = true;
                        _responseDto.Message = "User Created Successfully.";

                        if (!_roleManager.RoleExistsAsync("Customer").Result)
                        {
                            var roleRes = await _roleManager.CreateAsync(new ApplicationRole
                            {
                                Name = "Customer",
                                NormalizedName = "CUSTOMER",
                            });
                        }
                        var userroleRes = await _userManager.AddToRoleAsync(user, "Customer");
                    }
                    else
                    {
                        _responseDto.Message = res.Errors.FirstOrDefault().Description;
                    }
                }
                else
                {
                    _responseDto.Message = "Email already associated with us.";
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequest.UserName);
                if (user != null)
                {
                    var signInRes = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);
                    if (signInRes.Succeeded)
                    {
                        var loginRes = _mapper.Map<LoginResponseDto>(user);
                        var userRole = await _userManager.GetRolesAsync(user);
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim("UserId",user.Id.ToString() ?? ""),
                            new Claim(ClaimTypes.Name,user.Name ?? ""),
                            new Claim(ClaimTypes.MobilePhone,user.PhoneNumber ?? ""),
                            new Claim(ClaimTypes.Email,user.Email ?? ""),
                        };
                        foreach (var item in userRole)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item??""));
                            loginRes.Role.Add(item);
                        }
                        loginRes.Token = await _tokenGenerator.GenerateTokenAsync(claims);
                        var identity = new ClaimsIdentity(claims, "Cookies");
                        var property = new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddDays(7),
                            IsPersistent = true,
                            AllowRefresh = false
                        };
                        await _httpContextAccessor.HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(identity), property);

                        _responseDto.IsSuccess = true;
                        _responseDto.Message = "Login Susscessfully.";
                        _responseDto.Result = loginRes;
                    }
                    else
                    {
                        _responseDto.Message = "Invalid username or password.";
                    }
                }
                else
                {
                    _responseDto.Message = "Sorry,User not found.";
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            try
            {
                var roleRes = await _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper(),
                });
                if (roleRes.Succeeded)
                {
                    _responseDto.IsSuccess = true;
                }
                else
                {
                    _responseDto.Message = roleRes.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}
