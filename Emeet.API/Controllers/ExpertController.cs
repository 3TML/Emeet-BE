using Emeet.API.Constants;
using Emeet.Service.Interfaces;
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
        [Route("{ExperId}")]
        public async Task<IActionResult> GetExpertById([FromRoute] Guid ExperId)
        {
            try
            {
                var experts = await _expertService.GetExpertById(ExperId);
                return Ok(experts);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
