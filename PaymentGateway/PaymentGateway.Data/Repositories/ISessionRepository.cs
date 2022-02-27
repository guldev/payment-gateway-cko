using PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Data.Repositories
{
    public interface ISessionRepository
    {
        public Task<Session> CreateSession(int merchantId, string reference, string currency);
        Task<Session> GetByID(int sessionID);
    }
}
