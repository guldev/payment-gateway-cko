using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Constants
{
    public static class GatewayResponseCode
    {
        public static readonly string SUCCESS = "SUCCESS";

        public static readonly string SERVER_ERROR = "SERVER_ERROR";
        public static readonly string UNAUTHORISED = "UNAUTHORISED";

        #region Entities Not Found Codes

        //Merchant not found
        public static readonly string MERCHANT_NOTFOUND = "MERCHANT_NOTFOUND";

        //Transaction not found
        public static readonly string TRANSACTION_NOTFOUND = "TRANSACTION_NOTFOUND";

        //Session not found
        public static readonly string SESSION_NOTFOUND = "SESSION_NOTFOUND";

        //SessionId & Merchant ID Mismatch
        public static readonly string INVALID_SESS_MER = "INVALID_SESS_MER";

        #endregion

        #region Payment Transaction Error Codes

        //Payment has been already approved for the provided session
        public static readonly string DECLINED_PAYMENT_APPROVED = "DECLINED_PAYMENT_APPROVED";

        //Invalid payment amount provided
        public static readonly string INVALID_AMNT = "INVALID_AMNT";

        //Invalid currency
        public static readonly string INVALID_CURRENCY = "INVALID_CURRENCY";

        #endregion

        #region Credit Card Validation Error Codes

        //Invalid credit card number
        public static readonly string INVALID_CREDITCARDNO = "INVALID_CCN";

        //Invalid Card Verification value
        public static readonly string INVALID_CVV = "INVALID_CVV";

        //Invalid Card Expiry Date Formart
        public static readonly string INVALID_EXP_FRM = "INVALID_EXP_FRM";

        //Expired Card details provided
        public static readonly string EXPIRED_CARD = "EXPIRED_CARD";

        #endregion

        #region Acquiring Bank Calling Failure Codes

        //Error occured on calling the acquiring bank api
        public static readonly string ACQ_BANK_ERR = "ACQ_BANK_ERR";

        #endregion
    }
}
