using PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Data
{
    public static class DbInitializer
    {
        public static void Initialize(GatewayContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Merchants.Any())
            {
                return;   // DB has been seeded
            }

            var merchants = new Merchant[]
            {
                new Merchant (){ Name = "MerchantA", SecretKey = Guid.NewGuid().ToString(), IsActive = true, IsDeleted = false, CreatedDate = DateTime.Now},
                new Merchant (){ Name = "MerchantB", SecretKey = Guid.NewGuid().ToString(), IsActive = true, IsDeleted = false, CreatedDate = DateTime.Now}
            };
            foreach (Merchant m in merchants)
            {
                context.Merchants.Add(m);
            }

            context.SaveChanges();
        }
    }
}
