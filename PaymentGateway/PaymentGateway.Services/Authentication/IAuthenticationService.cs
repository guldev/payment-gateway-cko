using PaymentGateway.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Services.Authentication
{
    public interface IAuthenticationService
    {
        public Task<AuthenticationReponseModel> Authenticate(int merchandId, string secretKey);
    }
}
