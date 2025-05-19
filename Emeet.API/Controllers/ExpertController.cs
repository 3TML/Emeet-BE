using Emeet.API.Constants;
using Emeet.Service.DTOs.Requests.Expert;
using Emeet.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.Expert.ExpertEndpoint + "/[action]")]
    [ApiController]
    public class ExpertController : ControllerBase
    {
        private readonly IExpertService _expertService;

        public ExpertController(IExpertService expertService)
        {
            _expertService = expertService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSugestionExperts()
        {
            try
            {
                var experts = await _expertService.GetSuggestionExperts();
                return Ok(experts);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }

        [HttpGet]
        [Route("{experId}")]
        public async Task<IActionResult> GetExpertById([FromRoute] Guid experId)
        {
            try
            {
                var experts = await _expertService.GetExpertById(experId);
                return Ok(experts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetExpertByNameAndCategory([FromQuery] string name = "", [FromQuery] string catrgory = "", [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            try
            {
                var experts = await _expertService.GetExpertByNameCategory(name, catrgory, page, size);
                return Ok(experts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        //[Authorize(Roles = RoleNameAuthor.Expert)]
        public async Task<IActionResult> UploadCertificates(UploadCertificateRequest uploadRequest)
        {
            try
            {
                bool isUpload = await _expertService.UploadCertificate(uploadRequest);
                if (isUpload)
                {
                    return Ok("Upload certificates successfully");
                }
                else
                {
                    return BadRequest(new { message = "Failed to upload certificates" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        //[Authorize(Roles = RoleNameAuthor.Expert)]
        public async Task<IActionResult> DeleteCertificates(DeleteCertificateRequest deleteCertificate)
        {
            try
            {
                bool isDelete = await _expertService.DeleteCertificateById(deleteCertificate);
                if (isDelete)
                {
                    return Ok("Delete certificates successfully");
                }
                else
                {
                    return BadRequest( new { message = "Failed to delete certificates" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = RoleNameAuthor.Expert + "," + RoleNameAuthor.Admin)]
        [Route("{expertId:guid}")]
        public async Task<IActionResult> GetCertificatesByExpertId([FromRoute] Guid expertId, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            try
            {
                var result = await _expertService.GetCertificatesByExpertId(expertId, page, size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
