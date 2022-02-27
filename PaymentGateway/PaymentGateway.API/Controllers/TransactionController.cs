using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Core.Constants;
using PaymentGateway.Core.Models;
using PaymentGateway.Data.Repositories;
using PaymentGateway.Logging;
using PaymentGateway.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.API.Controllers
{
    [ApiController]
    [Route("api/transaction")]
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ILoggerService _loggerService;
        private readonly ISessionService _sessionService;
        private readonly ITransactionService _transactionService;

        public TransactionController
            (
            ILoggerService loggerService,
            ISessionService sessionService,
             ITransactionService transactionService)
        {
            _loggerService = loggerService;
            _sessionService = sessionService;
            _transactionService = transactionService;
        }

        [HttpPost("session")]
        public async Task<IActionResult> CreateSession([FromBody] CreateSessionRequestModel request)
        {
            _loggerService.LogInfo($"Payment session creation attempt for merchantID {request.MerchantId}");

            var session = await _sessionService.Create(request);

            if(session.GatewayResponseCode == BasicStatusCode.SUCCESS)
                _loggerService.LogInfo($"Payment session creation success for merchantID {request.MerchantId}");
            else
                _loggerService.LogInfo($"Payment session creation failure for merchantID {request.MerchantId}");

            return Ok(session);
        }

        [HttpPost("{merchantId}/pay/{sessionId}")]
        public async Task<IActionResult> Pay(int merchantId, int sessionId, CreateTransactionRequestModel request)
        {
            _loggerService.LogInfo($"Payment request submitted for merchantID {merchantId} with sessionID {sessionId}");

            var response = await _transactionService.CreatePaymentTransaction(request, merchantId, sessionId);

            _loggerService.LogInfo($"Payment request response for merchantID {merchantId} with sessionID {sessionId} : {response.GatewayResponseCode}");

            return Ok(response);
        }


        #region Listing/Reporting Endpoints


        [HttpGet("{merchantId}/{bankUniqueIdentifier}")]
        public async Task<IActionResult> GetTransactionByIdentifier(int merchantId, string bankUniqueIdentifier)
        {
            _loggerService.LogInfo($"Transaction detail request for merchantID {merchantId} with unique identifier {bankUniqueIdentifier}");

            var response = await _transactionService.GetTransactionByIdentifier(merchantId, bankUniqueIdentifier);

            return Ok(response);
        }

        #endregion
    }
}
