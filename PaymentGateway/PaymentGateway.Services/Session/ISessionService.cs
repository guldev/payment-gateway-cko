using PaymentGateway.Core.Models;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public interface ISessionService
    {
        public Task<CreateSessionResponseModel> Create(CreateSessionRequestModel request);
        Task<bool> IsSessionValid(int sessionId, int merchantID);
    }
}
