using Emeet.API.Constants;
using Emeet.Service.DTOs.Requests.ExpertService;
using Emeet.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.ExService.ExpertServiceEndpoint + "/[action]")]
    [ApiController]
    public class ExServiceController : ControllerBase
    {
        private readonly IExServiceService _service;

        public ExServiceController(IExServiceService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{expertId:guid}")]
        [Authorize(Roles = RoleNameAuthor.Expert + "," + RoleNameAuthor.Admin + "," + RoleNameAuthor.Customer)]
        public async Task<IActionResult> GetServiceByExpertId([FromRoute] Guid expertId)
        {
            try
            {
                var result = await _service.GetServiceByExpertId(expertId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleNameAuthor.Expert)]
        public async Task<IActionResult> CreateService(CreateServiceRequest request)
        {
            try
            {
                var result = await _service.CreateService(request);
                if (!result)
                {
                    return BadRequest("Failed to create service");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = RoleNameAuthor.Expert)]
        public async Task<IActionResult> UpdateService(Guid id, UpdateServiceRequest request)
        {
            try
            {
                var result = await _service.UpdateService(id, request);
                if (!result)
                {
                    return BadRequest("Failed to update service");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = RoleNameAuthor.Expert)]
        public async Task<IActionResult> DeleteServiceById(Guid id)
        {
            try
            {
                var result = await _service.DeleteServiceById(id);
                if (!result)
                {
                    return BadRequest("Failed to delete service");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
