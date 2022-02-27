
using PaymentGateway.Core.Entities;
using System.Threading.Tasks;

namespace PaymentGateway.Data.Repositories
{
    public interface IMerchantRepository
    {
        public Task<Merchant> GetByID(int merchantID);
        public Task<Merchant> GetByID(int merchantID, string secretKey);
        public Task<Merchant> GetByNameSecret(string merchantName, string secretKey);
    }
}
