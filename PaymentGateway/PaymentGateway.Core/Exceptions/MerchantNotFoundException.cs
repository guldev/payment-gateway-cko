using PaymentGateway.Core.Constants;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PaymentGateway.Core.Exceptions
{
    public class MerchantNotFoundException : BaseException
    {
        public MerchantNotFoundException() : base(GatewayResponseCode.MERCHANT_NOTFOUND, HttpStatusCode.NotFound)
        {
        }
    }
}
