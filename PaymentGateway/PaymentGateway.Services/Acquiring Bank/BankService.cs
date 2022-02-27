using Microsoft.Extensions.Options;
using PaymentGateway.Core.Constants;
using PaymentGateway.Core.Models;
using PaymentGateway.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class BankService : IBankService
    {
        private readonly AcquiringBankSettings _bankSettings;
        private readonly string _bankBaseUrl;
        private readonly IHttpClientFactory _httpClientFactory;

        public BankService
            (IHttpClientFactory httpClientFactory,
             IOptions<AcquiringBankSettings> bankSettings)
        {
            _bankSettings = bankSettings.Value;
            _bankBaseUrl = _bankSettings.BaseUrl;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<BankPaymentResponseModel> SendPaymentRequest(BankPaymentRequestModel request)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("Default");
                var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

                string paymentReqEndpint = $"{_bankBaseUrl}/api/payment";
                var response = await httpClient.PostAsJsonAsync(paymentReqEndpint, request);

                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return new BankPaymentResponseModel() { Status = GatewayResponseCode.ACQ_BANK_ERR };
                }

                var data = await response.Content.ReadAsAsync<BankPaymentResponseModel>();

                return data;
            }
            catch (Exception)
            {
                return new BankPaymentResponseModel() { Status = GatewayResponseCode.ACQ_BANK_ERR };
            }
        }
    }
}
