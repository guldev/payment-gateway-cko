using Microsoft.EntityFrameworkCore;
using PaymentGateway.Core.Entities;
using PaymentGateway.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Data.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly GatewayContext _context;

        public MerchantRepository(GatewayContext context)
        {
            _context = context;
        }

        public async Task<Merchant> GetByID(int merchantID)
        {
            var merchant = await _context.Merchants.FindAsync(merchantID);
            
            if(merchant == null) throw new MerchantNotFoundException();

            return merchant;
        }

        public async Task<Merchant> GetByID(int merchantID, string secretKey)
        {
            var merchant = await _context.Merchants
                                .FirstOrDefaultAsync
                                (x => x.ID == merchantID && x.SecretKey == secretKey && x.IsActive && !x.IsDeleted);

            return merchant;
        }
    }
}
