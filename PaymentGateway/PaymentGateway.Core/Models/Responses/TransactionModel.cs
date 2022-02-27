using PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Models
{
    public class TransactionModel
    {
        public string BankUniqueIdentifier { get; set; }
        public int MerchantID { get; set; }
        public int SessionID { get; set; }
        public string CardNumber { get; set; }
        public string  ExpiryDate { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string TransactionDatetime { get; set; }
        public string GatewayResponseCode { get; set; }

        public TransactionModel()
        {

        }

        public TransactionModel(Transaction transaction)
        {
            BankUniqueIdentifier = transaction.BankUniqueIdentifier;
            MerchantID = transaction.MerchantID;
            SessionID = transaction.SessionID;
            CardNumber = transaction.CardNumber;
            ExpiryDate = transaction.Expiry;
            Amount = transaction.Amount;
            Currency = transaction.Currency;
            TransactionDatetime = transaction.CreatedDate.ToString("dd-MM-yyyy HH:mm:ss");
            GatewayResponseCode = transaction.GatewayResponseCode;
        }
    }
}
