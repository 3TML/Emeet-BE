using Emeet.API.Constants;
using Emeet.Service.DTOs.Requests.Authentication;
using Emeet.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
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

        [HttpPost]
        public async Task<IActionResult> RegisterAccount([FromBody] Service.DTOs.Requests.Authentication.RegisterRequest request)
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

        [HttpPost]
        public async Task<IActionResult> LoginByPassword([FromBody] LoginPasswordRequest request)
        {
            try
            {
                var result = await _authenticationService.LoginPassword(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
