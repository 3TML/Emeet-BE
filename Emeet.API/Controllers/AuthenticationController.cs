using Emeet.API.Constants;
using Emeet.Service.DTOs.Requests.Authentication;
using Emeet.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.Authentication.AuthenticationEndpoint + "/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> TestAPI()
        {
            return Ok("ditme may Khang và Hoảng");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterRequest request)
        {
            try
            {
                bool result = await _authenticationService.RegisterUser(request);
                return Ok("Đăng ký tài khoản thành công");
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
