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
        public string Status { get; set; }

        public CreateTransactionResponseModel()
        {

        }

        public CreateTransactionResponseModel(string status)
        {
            Status = status;
        }

        public CreateTransactionResponseModel(Transaction transaction)
        {
            BankUniqueIdentifier = transaction.BankUniqueIdentifier;
            TransactionId = transaction.ID;
            Status = transaction.StatusCode;
        }
    }
}
