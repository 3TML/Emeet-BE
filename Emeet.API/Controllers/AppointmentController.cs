using Emeet.API.Constants;
using Emeet.Domain.Exceptions;
using Emeet.Service.DTOs.Requests.Appointment;
using Emeet.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.Appointment.AppointmentEndpoint + "/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Authorize(Roles = RoleNameAuthor.Customer)]
        public async Task<IActionResult> GetAvailableTime([FromQuery] GetAvailableTimeRequest request)
        {
            try
            {
                var result = await _appointmentService.GetAvailableTime(request);
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

        [HttpPost]
        [Authorize(Roles = RoleNameAuthor.Customer)]
        public async Task<IActionResult> BookExpert([FromQuery] BookExperRequest request)
        {
            try
            {
                var result = await _appointmentService.BookExpert(request);
                if (!result)
                {
                    return BadRequest(new { message = "Failed to book expert" });
                }
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

        [HttpGet]
        [Route("{customerId:guid}")]
        [Authorize(Roles = RoleNameAuthor.Admin + "," + RoleNameAuthor.Customer)]
        public async Task<IActionResult> GetAppointmentByCustomerId([FromRoute] Guid customerId, [FromQuery] DateTime? date, [FromQuery] string expertName = "", [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            try
            {
                var result = await _appointmentService.GetAppointmentByCustomerId(customerId, date, expertName, page, size);
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

        [HttpGet]
        [Route("{expertId:guid}")]
        [Authorize(Roles = RoleNameAuthor.Admin + "," + RoleNameAuthor.Expert)]
        public async Task<IActionResult> GetAppointmentByExpertId([FromRoute] Guid expertId, [FromQuery] DateTime? date, [FromQuery] string expertName = "", [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            try
            {
                var result = await _appointmentService.GetAppointmentByExpertId(expertId, date, expertName, page, size);
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


        //[HttpGet]
        //public async Task<IActionResult> GetLinkGGMEET()
        //{
        //    try
        //    {
        //        var result = await _appointmentService.CreateLinkGgMeet();
        //        return Ok(result);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}
    }
}
