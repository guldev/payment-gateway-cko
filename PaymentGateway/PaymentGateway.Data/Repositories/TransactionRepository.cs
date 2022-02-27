using Microsoft.EntityFrameworkCore;
using PaymentGateway.Core.Constants;
using PaymentGateway.Core.Entities;
using PaymentGateway.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly GatewayContext _gatewayContext;

        public TransactionRepository(GatewayContext gatewayContext)
        {
            _gatewayContext = gatewayContext;
        }

        public async Task<Transaction> Create(Transaction transaction)
        {
            await _gatewayContext.Transactions.AddAsync(transaction);
            await _gatewayContext.SaveChangesAsync();

            return transaction;
        }

        public Task<Transaction> GetTransactionByReference(string reference)
        {
            throw new NotImplementedException();
        }

        public async Task<Transaction> GetTransactionByUniqueId(int merchantId, string identifier)
        {
            var transaction = await _gatewayContext.Transactions
                             .FirstOrDefaultAsync
                             (x => x.BankUniqueIdentifier == identifier && x.MerchantID == merchantId);

            if (transaction == null) throw new TransactionNotFoundException();

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByMerchant(int merchantId)
        {
            var transactions = await _gatewayContext.Transactions
                               .Where(x => x.MerchantID == merchantId).ToListAsync();

            return transactions;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionBySession(int sessionID)
        {
            var transactions = await _gatewayContext.Transactions
                                    .Where(x => x.SessionID == sessionID)
                                    .ToListAsync();

            return transactions;
        }


        public async Task<IEnumerable<Transaction>> GetApprovedTransactionBySession(int sessionID)
        {
            var transactions = await _gatewayContext.Transactions
                                    .Where(x => x.SessionID == sessionID && x.StatusCode == BankResponseCode.APPROVED)
                                    .ToListAsync();

            return transactions;
        }

        public async Task SaveChanges()
        {
            await _gatewayContext.SaveChangesAsync();
        }
    }
}
