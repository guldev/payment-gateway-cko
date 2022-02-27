using Microsoft.EntityFrameworkCore;
using PaymentGateway.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Data
{
    public class GatewayContext : DbContext
    {
        public GatewayContext(DbContextOptions<GatewayContext> options) : base(options)
        {

        }

        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Merchant>().ToTable("Merchant");
            modelBuilder.Entity<Session>().ToTable("Session");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
        }
    }
}
