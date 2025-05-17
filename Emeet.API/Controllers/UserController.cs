using Emeet.API.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.User.UserEndpoint + "/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

    }
}
