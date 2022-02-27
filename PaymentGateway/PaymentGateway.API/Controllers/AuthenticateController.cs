using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.Services;
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
        private readonly ITokenService _tokenService;

        public AuthenticateController
            (IAuthenticationService authenticationService, 
            ILoggerService loggerService, 
            ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _loggerService = loggerService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestModel request)
        {
            _loggerService.LogInfo($"Authentication attempt for merchantID {request.MerchantName}");

            var response = await _authenticationService.Authenticate(request.MerchantName, request.SecretKey);

            if(response == null)
                _loggerService.LogInfo($"Authentication failure for merchantID {request.MerchantName}");
            else
            {
                _loggerService.LogInfo($"Authentication success for merchantID {request.MerchantName}");
                _loggerService.LogInfo($"Creating access token for merchantID {request.MerchantName}");
                
                string accessToken = _tokenService.CreateAccessToken();
                response.Token = accessToken;
            }

            return Ok(response);
        }
    }
}
