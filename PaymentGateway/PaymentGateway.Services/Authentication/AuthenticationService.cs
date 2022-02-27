using PaymentGateway.Core.Models;
using PaymentGateway.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMerchantRepository _merchantRepository;

        public AuthenticationService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task<AuthenticationReponseModel> Authenticate(int merchantId, string secretKey)
        {
            var merchant = await _merchantRepository.GetByID(merchantId, secretKey);

            if (merchant == null) return null;

            return new AuthenticationReponseModel() { MerchantId = merchantId, Token = "" };
        }
    }
}
