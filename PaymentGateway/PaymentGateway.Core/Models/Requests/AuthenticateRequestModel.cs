using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PaymentGateway.Core.Models
{
    public class AuthenticateRequestModel
    {
        [Required]
        public string MerchantName { get; set; }

        [Required]
        public string SecretKey { get; set; }
    }
}
