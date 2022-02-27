using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcquiringBank.API.Models
{
    public class PaymentResponseModel
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }

        public PaymentResponseModel()
        {

        }

        public PaymentResponseModel(string status)
        {
            TransactionId = Guid.NewGuid().ToString();
            Status = status;
        }
    }
}
