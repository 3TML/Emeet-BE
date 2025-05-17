using Emeet.API.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.Authentication.AuthenticationEndpoint + "/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        public AppointmentController()
        {
        }
    }
}
