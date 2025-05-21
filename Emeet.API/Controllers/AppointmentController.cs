using Emeet.API.Constants;
using Emeet.Domain.Exceptions;
using Emeet.Service.DTOs.Requests.Appointment;
using Emeet.Service.Interfaces;
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
        //[Authorize(Roles = RoleNameAuthor.Admin + "," + RoleNameAuthor.Customer + "," + RoleNameAuthor.Admin)]
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
        //[Authorize(Roles = RoleNameAuthor.Customer)]
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
