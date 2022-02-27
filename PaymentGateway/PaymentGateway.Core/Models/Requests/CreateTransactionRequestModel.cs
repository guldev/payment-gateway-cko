using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Models
{
    public class CreateTransactionRequestModel
    {
        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public string Currency { get; set; }
        public string Cvv { get; set; }
        public decimal Amount { get; set; }
    }

}
