using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Entities
{
    public class Session : BaseEntity
    {
        public string Reference { get; set; }
        public string Currency { get; set; }
        public int MerchantID { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
