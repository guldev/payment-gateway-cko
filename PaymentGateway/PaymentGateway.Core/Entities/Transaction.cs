using PaymentGateway.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public int SessionID { get; set; }
        public int MerchantID { get; set; }
        public TransactionType Type { get; set; }
        public string CardNumber { get; set; }
        public string Expiry { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string BankUniqueIdentifier { get; set; }
        public string StatusCode { get; set; }
    }
}
