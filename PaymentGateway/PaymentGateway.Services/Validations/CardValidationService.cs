using PaymentGateway.Core.Constants;
using PaymentGateway.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class CardValidationService : ICardValidationService
    {
        public ValidationResult
            ValidateCardDetails(string cardNumber, string expiry, string cvv, string currency)
        {

            if (!CreditCardCheck(cardNumber))
                return new ValidationResult(false, GatewayResponseCode.INVALID_CREDITCARDNO);

            if (!CVVCheck(cvv))
                return new ValidationResult(false, GatewayResponseCode.INVALID_CVV);

            if (!ExpiryDateFormatCheck(expiry))
                return new ValidationResult(false, GatewayResponseCode.INVALID_EXP_FRM);

            if (!ExpiryDateCheck(expiry))
                return new ValidationResult(false, GatewayResponseCode.EXPIRED_CARD);

            if (!CurrencyCheck(currency))
                return new ValidationResult(false, GatewayResponseCode.INVALID_CURRENCY);

            return new ValidationResult(true, GatewayResponseCode.SUCCESS);
        }

        public bool CreditCardCheck(string cardNumber)
        {
            return LuhnValidate(cardNumber);
        }

        public bool CVVCheck(string cvv)
        {
            var cvvCheck = new Regex(@"^\d{3}$");

            return cvvCheck.IsMatch(cvv);
        }
        
        public bool CurrencyCheck(string currency)
        {
            return currency.Length == 3 && Regex.IsMatch(currency, @"^[a-zA-Z]+$");
        }

        public bool ExpiryDateFormatCheck(string expiryDate)
        {
            var monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
            var yearCheck = new Regex(@"^20[0-9]{2}$");

            var dateParts = expiryDate.Split('/'); 
            if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1])) 
                return false; 

            return true;
        }

        public bool ExpiryDateCheck(string expiryDate)
        {
            var dateParts = expiryDate.Split('/'); 
            var year = int.Parse(dateParts[1]);
            var month = int.Parse(dateParts[0]);
            var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month); 
            var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

            //check expiry greater than today & within next 6 years <7, 8>>
            return cardExpiry > DateTime.Now;
        }

        private static bool LuhnValidate(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                char[] nx = cardNumber.ToArray();
                int n = 0;
                if (!int.TryParse(nx[i].ToString(), out n)) return false;

                if (alternate)
                {
                    n *= 2;

                    if (n > 9)
                    {
                        n = (n % 10) + 1;
                    }
                }
                sum += n;
                alternate = !alternate;
            }
            return (sum % 10 == 0);
        }
    }
}
