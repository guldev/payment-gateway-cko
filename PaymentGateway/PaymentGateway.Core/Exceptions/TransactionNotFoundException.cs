using PaymentGateway.Core.Constants;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PaymentGateway.Core.Exceptions
{
    public class TransactionNotFoundException : BaseException
    {
        public TransactionNotFoundException() : base(GatewayResponseCode.TRANSACTION_NOTFOUND, HttpStatusCode.NotFound)
        {
        }
    }
}
