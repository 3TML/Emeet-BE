using Emeet.API.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.ExService.ExpertServiceEndpoint + "/[action]")]
    [ApiController]
    public class ExServiceController : ControllerBase
    {
        public ExServiceController()
        {
        }
    }
}
