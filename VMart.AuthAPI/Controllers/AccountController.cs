using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VMart.AuthAPI.Models;
using VMart.AuthAPI.Models.Dto;

namespace VMart.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ResponseDto _responseDto;
        private readonly IMapper _mapper;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _responseDto = new ResponseDto { Message = "Sorry,Something went wrong try after sometime.", Result = null };
            _mapper = mapper;
            _roleManager = roleManager;
        }
        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(UserRegistrationReq registrationReq)
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
            return Ok(_responseDto);
        }
    }
}
