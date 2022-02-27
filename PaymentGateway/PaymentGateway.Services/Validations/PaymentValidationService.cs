using PaymentGateway.Core.Constants;
using PaymentGateway.Core.Models;
using PaymentGateway.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class PaymentValidationService : IPaymentValidationService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ISessionService _sessionService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICardValidationService _cardValidationService;

        public PaymentValidationService(
              IMerchantRepository merchantRepository,
              ISessionService sessionService,
              ITransactionRepository transactionRepository,
              ICardValidationService cardValidationService)
        {
            _merchantRepository = merchantRepository;
            _sessionService = sessionService;
            _transactionRepository = transactionRepository;
            _cardValidationService = cardValidationService;
        }

        public async Task<ValidationResult> ValidatePaymentRequest(int merchantID, int sessionID, CreateTransactionRequestModel req)
        {
            // Retrieval of the merchant & session entity serves to check if the IDs provided are valid
            // If not a not found exception will be thrown
            var merchant = await _merchantRepository.GetByID(merchantID);
            var session = await _sessionService.IsSessionValid(sessionID, merchantID);


            // The provided card details are validated for the cc number, expiry date, cvv & currency
            var validationResult = _cardValidationService
                                     .ValidateCardDetails(req.CardNumber, req.Expiry, req.Cvv, req.Currency);

            if (!validationResult.IsValid)
                return validationResult;

            //Validate Payment Transaction Amount
            if (req.Amount <= 0)
                return new ValidationResult(false, GatewayResponseCode.INVALID_AMNT);

            // Assumption: Only one approved payment per session is allowed 
            // Check if a payment has already been previously approved by the bank for the current session
            // If that is true, payment request should be rejected
            var transaction = await _transactionRepository.GetApprovedTransactionBySession(sessionID);
            if(transaction.Any())
                return new ValidationResult(false, GatewayResponseCode.DECLINED_PAYMENT_APPROVED);

            return new ValidationResult(true, GatewayResponseCode.SUCCESS);
        }
    }
}
