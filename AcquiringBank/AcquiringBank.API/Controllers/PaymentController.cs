using AcquiringBank.API.Constants;
using AcquiringBank.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcquiringBank.API.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        [HttpPost]
        public IActionResult Pay([FromBody] PaymentRequestModel requestModel)
        {
            return Ok(new PaymentResponseModel(PaymentResponseCode.APPROVED));
        }
    }
}
