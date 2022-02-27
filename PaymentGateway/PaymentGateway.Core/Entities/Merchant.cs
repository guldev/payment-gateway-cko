using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Entities
{
    public class Merchant : BaseEntity
    {
        public string Name { get; set; }
        public string SecretKey { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
