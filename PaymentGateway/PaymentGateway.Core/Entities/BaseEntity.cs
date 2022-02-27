using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
