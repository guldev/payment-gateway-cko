using Microsoft.EntityFrameworkCore;
using PaymentGateway.Core.Entities;
using PaymentGateway.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Data.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly GatewayContext _gatewayContext;

        public SessionRepository(GatewayContext gatewayContext)
        {
            _gatewayContext = gatewayContext;
        }

        public async Task<Session> CreateSession(int merchantId, string reference, string currency)
        {
            var session = new Session()
            {
                MerchantID = merchantId,
                Reference = reference,
                Currency = currency,
                CreatedDate = DateTime.Now
            };

            var task = await _gatewayContext.Sessions.AddAsync(session);
            await _gatewayContext.SaveChangesAsync();

            return session;
        }

        public async Task<Session> GetByID(int sessionID)
        {
            var session = await _gatewayContext.Sessions.FirstOrDefaultAsync(x => x.ID == sessionID);

            if (session == null) throw new SessionNotFoundException();

            return session;
        }
    }
}
