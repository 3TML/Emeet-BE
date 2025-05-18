using Emeet.API.Constants;
using Emeet.Domain.Exceptions;
using Emeet.Service.DTOs.Requests.User;
using Emeet.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.User.UserEndpoint + "/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = RoleNameAuthor.Admin)]
        public async Task<IActionResult> GetAllUser([FromQuery] string fullName = "",[FromQuery] string role = "", [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            try
            {
                var result = await _userService.GetAllUser(fullName, role, page, size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = RoleNameAuthor.Customer + "," + RoleNameAuthor.Expert)]
        public async Task<IActionResult> UpdateProfileById([FromRoute] Guid id, [FromForm] UpdateUserRequest updateUserRequest)
        {
            try
            {
                var result = await _userService.UpdateProfileById(id, updateUserRequest);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
