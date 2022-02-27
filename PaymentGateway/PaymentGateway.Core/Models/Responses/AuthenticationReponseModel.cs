using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Models
{
    public class AuthenticationReponseModel
    {
        public int MerchantId { get; set; }
        public string Token { get; set; }
    }
}
