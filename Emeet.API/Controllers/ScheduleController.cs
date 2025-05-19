using Emeet.API.Constants;
using Emeet.Service.DTOs.Requests.Schedule;
using Emeet.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.Schedule.ScheduleEndpoint + "/[action]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        [Authorize(Roles = RoleNameAuthor.Expert)]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleRequest request)
        {
            try
            {
                var result = await _scheduleService.CreateSchedule(request);
                if (result)
                {
                    return Ok(result);
                }
                return BadRequest(new { message = "Failed to create schedule" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("{expertId:guid}")]
        [Authorize(Roles = RoleNameAuthor.Admin + "," + RoleNameAuthor.Expert + "," + RoleNameAuthor.Customer)]
        public async Task<IActionResult> GetScheduleByExpertId([FromRoute] Guid expertId)
        {
            try
            {
                var result = await _scheduleService.GetScheduleByExpertId(expertId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = RoleNameAuthor.Admin + "," + RoleNameAuthor.Expert + "," + RoleNameAuthor.Customer)]
        public async Task<IActionResult> UpdateSchedule([FromRoute] Guid id, [FromBody] UpdateScheduleRequest request)
        {
            try
            {
                var result = await _scheduleService.UpdateSchedule(id, request);
                if (!result)
                {
                    return BadRequest("Failed to update schedule");
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
