using Emeet.API.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.Feedback.FeedbackEndpoint + "/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        public FeedbackController()
        {
        }
    }
}
