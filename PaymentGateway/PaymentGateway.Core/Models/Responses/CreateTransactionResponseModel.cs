using PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Models
{
    public class CreateTransactionResponseModel
    {
        public string BankUniqueIdentifier { get; set; }
        public int TransactionId { get; set; }
        public string GatewayResponseCode { get; set; }

        public CreateTransactionResponseModel()
        {

        }

        public CreateTransactionResponseModel(string status)
        {
            GatewayResponseCode = status;
        }

        public CreateTransactionResponseModel(Transaction transaction)
        {
            BankUniqueIdentifier = transaction.BankUniqueIdentifier;
            TransactionId = transaction.ID;
            GatewayResponseCode = transaction.GatewayResponseCode;
        }
    }
}
