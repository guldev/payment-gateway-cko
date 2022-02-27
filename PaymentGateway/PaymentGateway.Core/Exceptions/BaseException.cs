using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PaymentGateway.Core.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public BaseException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
