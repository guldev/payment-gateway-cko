using PaymentGateway.Core.Models;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public interface IBankService
    {
        Task<BankPaymentResponseModel> SendPaymentRequest(BankPaymentRequestModel request);
    }
}