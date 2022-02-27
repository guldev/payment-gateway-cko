using PaymentGateway.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public interface ITransactionService
    {
        public Task<CreateTransactionResponseModel> CreatePaymentTransaction(CreateTransactionRequestModel request, int merchantID, int sessionID);
        Task<TransactionModel> GetTransactionByIdentifier(int merchantId, string identifier);
        Task<List<TransactionModel>> GetTransactions(int merchantId);
    }
}
