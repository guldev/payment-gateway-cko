using PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Data.Repositories
{
    public interface ITransactionRepository
    {
        public Task<Transaction> Create(Transaction transaction);
        public Task<IEnumerable<Transaction>> GetTransactionsByMerchant(int merchantId);
        public Task<Transaction> GetTransactionByUniqueId(int merchantId, string identifier);
        public Task<Transaction> GetTransactionByReference(string reference);
        Task<IEnumerable<Transaction>> GetTransactionBySession(int sessionID);
        Task<IEnumerable<Transaction>> GetApprovedTransactionBySession(int sessionID);
        public Task SaveChanges();
    }
}
