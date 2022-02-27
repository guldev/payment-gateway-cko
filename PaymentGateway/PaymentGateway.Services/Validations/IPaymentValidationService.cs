using PaymentGateway.Core.Models;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public interface IPaymentValidationService
    {
        Task<ValidationResult> ValidatePaymentRequest(int merchantID, int sessionID, CreateTransactionRequestModel req);
    }
}