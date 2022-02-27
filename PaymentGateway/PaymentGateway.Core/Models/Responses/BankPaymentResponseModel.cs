using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Models
{
    public class BankPaymentResponseModel
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
    }
}
