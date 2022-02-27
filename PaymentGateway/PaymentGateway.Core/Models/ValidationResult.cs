using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ValidationErrorCode { get; set; }

        public ValidationResult(bool isValid, string code)
        {
            IsValid = isValid;
            ValidationErrorCode = code;
        }
    }
}
