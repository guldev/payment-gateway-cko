using PaymentGateway.Core.Constants;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Core.Models;
using PaymentGateway.Data.Repositories;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMerchantRepository _merchantRepository;

        public SessionService
            (ISessionRepository sessionRepository,
             IMerchantRepository merchantRepository)
        {
            _sessionRepository = sessionRepository;
            _merchantRepository = merchantRepository;
        }

        public async Task<CreateSessionResponseModel> Create(CreateSessionRequestModel request)
        {
            //Check if session with provided reference already exists
            var sessionWithReference = await _sessionRepository.GetByReference(request.Reference);

            if (sessionWithReference != null)
                throw new BaseException(GatewayResponseCode.SESSION_REF_EXISTS, HttpStatusCode.BadRequest);

            var merchant = await _merchantRepository.GetByID(request.MerchantId);

            var session = await _sessionRepository.CreateSession(request.MerchantId, request.Reference, request.Currency);

            return new CreateSessionResponseModel(session);
        }

        public async Task<bool> IsSessionValid(int sessionId, int merchantID)
        {
            var session = await _sessionRepository.GetByID(sessionId);

            if(session.MerchantID != merchantID) 
                throw new BaseException(GatewayResponseCode.INVALID_SESS_MER, HttpStatusCode.BadRequest);

            return true;
        }
    }
}
