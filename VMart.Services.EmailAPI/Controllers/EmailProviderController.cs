using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VMart.Services.EmailAPI.Models.DTOs;
using VMart.Services.EmailAPI.Services.IService;

namespace VMart.Services.EmailAPI.Controllers
{
    [Route("api/emailprovider")]
    [ApiController]
    public class EmailProviderController : ControllerBase
    {
        private readonly IEmailProvider _emailProvider;
        public EmailProviderController(IEmailProvider emailProvider)
        {
            _emailProvider = emailProvider;
        }
        [HttpPost(nameof(Send))]
        public async Task<IActionResult> Send(SendDto sendDto)
        {
            var res = await _emailProvider.send(sendDto);
            return Ok(res);
        }
    }
}
