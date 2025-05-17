using Emeet.API.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emeet.API.Controllers
{
    [Route(ApiEndPointConstant.Payment.PaymentEndpoint + "/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public PaymentController()
        {
        }
    }
}
