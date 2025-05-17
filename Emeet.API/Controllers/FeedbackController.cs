using Emeet.API.Constants;
using Emeet.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.Feedback.FeedbackEndpoint + "/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        [Route("{ExpertId}")]
        public async Task<IActionResult> GetFeedbackExpert([FromRoute]Guid ExpertId, [FromQuery]int page = 1, [FromQuery]int size = 10)
        {
            try
            {
                var feedbacks = await _feedbackService.GetFeedbackExpert(ExpertId, page, size);
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
