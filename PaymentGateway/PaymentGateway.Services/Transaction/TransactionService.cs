using PaymentGateway.Core.Entities;
using PaymentGateway.Core.Enums;
using PaymentGateway.Core.Models;
using PaymentGateway.Data.Repositories;
using PaymentGateway.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ISessionService _sessionService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentValidationService _paymentValidationService;
        private readonly IBankService _bankService;

        public TransactionService(
              IMerchantRepository merchantRepository,
              ISessionService sessionService,
              ITransactionRepository transactionRepository,
              IPaymentValidationService paymentValidationService,
              IBankService bankService)
        {
            _merchantRepository = merchantRepository;
            _sessionService = sessionService;
            _transactionRepository = transactionRepository;
            _paymentValidationService = paymentValidationService;
            _bankService = bankService;
        }

        #region Create Payment Transaction
        public async Task<CreateTransactionResponseModel> CreatePaymentTransaction(CreateTransactionRequestModel req, int merchantID, int sessionID)
        {
            // Validate Payment Request details such credit card details, payment amount & IDs
            var validationResult = await _paymentValidationService.ValidatePaymentRequest(merchantID, sessionID, req);

            if (!validationResult.IsValid)
                return new CreateTransactionResponseModel(validationResult.ValidationErrorCode);

            // If card details are valid a transaction is logged to the database
            // Credit card number & expiry date are masked, whereas the CVV is not saved
            var transaction = new Transaction()
            {
                SessionID = sessionID,
                MerchantID = merchantID,
                CardNumber = MaskCCNumber(req.CardNumber),
                Expiry = MaskExpiry(req.Expiry),
                Amount = req.Amount,
                Currency = req.Currency,
                Type = TransactionType.Pay,
                CreatedDate = DateTime.Now
            };

            transaction = await _transactionRepository.Create(transaction);

            // Once a transaction has been logged, the payment request call is sent to the acquiring bank API
            var paymentRequestModel = new BankPaymentRequestModel();

            var response = await _bankService.SendPaymentRequest(paymentRequestModel);

            //transaction is then updated with the response received from the bank
            transaction.BankUniqueIdentifier = response.TransactionId;
            transaction.StatusCode = response.Status;
            await _transactionRepository.SaveChanges();

            return new CreateTransactionResponseModel(transaction);
        }

        private string MaskCCNumber(string CreditCardNumber)
        {
            CreditCardNumber = string.Format("************{0}", CreditCardNumber.Trim().Substring(12, 4));

            return CreditCardNumber;
        }

        private string MaskExpiry(string expiryDate)
        {
            expiryDate = string.Format("**/**{0}", expiryDate.Trim().Substring(4, 2));

            return expiryDate;
        }
        #endregion


        #region Retrieve Transaction

        public async Task<TransactionModel> GetTransactionByIdentifier(int merchantId, string identifier)
        {
            var transaction = await _transactionRepository.GetTransactionByUniqueId(merchantId, identifier);

            return new TransactionModel(transaction);
        }

        public async Task<List<TransactionModel>> GetTransactions(int merchantId)
        {
            var transactions = (await _transactionRepository.GetTransactionsByMerchant(merchantId)).ToList();

            var transactionModels = transactions.Select(x => new TransactionModel(x)).ToList();

            return transactionModels;
        }

        #endregion
    }


}
