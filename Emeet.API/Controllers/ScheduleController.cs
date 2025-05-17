using Emeet.API.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.Schedule.ScheduleEndpoint + "/[action]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        public ScheduleController()
        {
        }
    }
}
