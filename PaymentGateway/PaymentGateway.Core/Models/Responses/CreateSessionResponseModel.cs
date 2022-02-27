using PaymentGateway.Core.Constants;
using PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Models
{
    public class CreateSessionResponseModel
    {
        public int MerchantId { get; set; }
        public int SessionID { get; set; }
        public string Status { get; set; }

        public CreateSessionResponseModel()
        {

        }

        public CreateSessionResponseModel(Session session)
        {
            MerchantId = session.MerchantID;
            SessionID = session.ID;
            Status = BasicStatusCode.SUCCESS;
        }
    }
}
