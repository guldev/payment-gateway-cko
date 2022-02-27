using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Core.Models;
using PaymentGateway.Logging;
using PaymentGateway.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Controllers
{
    [ApiController]
    [Route("api/authenticate")]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggerService _loggerService;

        public AuthenticateController(IAuthenticationService authenticationService, ILoggerService loggerService)
        {
            _authenticationService = authenticationService;
            _loggerService = loggerService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestModel request)
        {
            _loggerService.LogInfo($"Authentication attempt for merchantID {request.MerchantId}");

            var response = await _authenticationService.Authenticate(request.MerchantId, request.SecretKey);

            if(response == null)
                _loggerService.LogInfo($"Authentication failure for merchantID {request.MerchantId}");
            else
                _loggerService.LogInfo($"Authentication success for merchantID {request.MerchantId}");

            return Ok(response);
        }
    }
}
