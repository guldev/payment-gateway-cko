using PaymentGateway.Core.Constants;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PaymentGateway.Core.Exceptions
{
    public class UnauthorisedException : BaseException
    {
        public UnauthorisedException() : base(GatewayResponseCode.UNAUTHORISED, HttpStatusCode.Unauthorized)
        {
        }
    }
}
