using AutoMapper;
using Emeet.API.Constants;
using Emeet.Domain.Exceptions;
using Emeet.Service.DTOs.Requests.Email;
using Emeet.Service.DTOs.Requests.OTP;
using Emeet.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.OTP.OTPEndpoint + "/[action]")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public OTPController(IMapper mapper, IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendOTPToEmail([FromBody] SendOtpEmailRequest sendOtpEmailRequest)
        {
            try
            {
                bool isSendOtp = await _emailService.SendEmailAsync(sendOtpEmailRequest);
                if (!isSendOtp)
                {
                    return BadRequest("Cannot send mail!");
                }
                return Ok("Send Otp successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> checkOtp([FromBody] CheckOtpRequest CheckOtpRequest)
        {
            try
            {
                bool isValidOtp = await _emailService.CheckOtpEmail(CheckOtpRequest);
                if (!isValidOtp)
                {
                    return BadRequest(new { message = "Otp không đúng. Vui lòng nhập lại" });
                }
                return Ok("Otp is valid!");
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
        public async Task<IActionResult> CheckExistEmail([FromBody] CheckExistEmailResrequest request)
        {
            try
            {
                bool isExistEmail = await _emailService.CheckExistEmail(request);
                if (!isExistEmail)
                {
                    return Ok("Email hợp lệ");
                }
                return Ok("Email đã tồn tại trên hệ thống!");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = RoleNameAuthor.Admin + "," + RoleNameAuthor.Expert + "," + RoleNameAuthor.Customer)]
        public async Task<IActionResult> CheckNonExistEmail([FromBody] CheckExistEmailResrequest request)
        {
            try
            {
                bool isExistEmail = await _emailService.CheckExistEmail(request);
                if (isExistEmail)
                {
                    return Ok("gửi OTP thành công");
                }
                return Ok("Email không tồn tại trên hệ thống!");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
