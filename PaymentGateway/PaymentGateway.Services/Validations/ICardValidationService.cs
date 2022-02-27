using PaymentGateway.Core.Models;

namespace PaymentGateway.Services
{
    public interface ICardValidationService
    {
        bool CreditCardCheck(string cardNumber);
        bool CVVCheck(string cvv);
        bool ExpiryDateCheck(string expiryDate);
        bool ExpiryDateFormatCheck(string expiryDate);
        ValidationResult ValidateCardDetails(string cardNumber, string expiry, string cvv, string currency);
    }
}