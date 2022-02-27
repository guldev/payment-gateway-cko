using PaymentGateway.Core.Constants;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PaymentGateway.Core.Exceptions
{
    public class SessionNotFoundException : BaseException
    {
        public SessionNotFoundException() : base(GatewayResponseCode.SESSION_NOTFOUND, HttpStatusCode.NotFound)
        {
        }
    }
}
