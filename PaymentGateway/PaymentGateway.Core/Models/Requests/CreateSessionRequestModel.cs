using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentGateway.Core.Models
{
    public class CreateSessionRequestModel
    {
        [Required]

        public int MerchantId { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        public string Currency { get; set; }
    }
}
