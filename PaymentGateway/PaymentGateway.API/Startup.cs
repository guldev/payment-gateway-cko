using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using PaymentGateway.API.Middlewares;
using PaymentGateway.Core.Settings;
using PaymentGateway.Data;
using PaymentGateway.Data.Repositories;
using PaymentGateway.Logging;
using PaymentGateway.Services;
using PaymentGateway.Services.Authentication;
using System;
using System.IO;

namespace PaymentGateway.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<GatewayContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #region Settings

            services.Configure<AcquiringBankSettings>(Configuration.GetSection("AcquiringBank"));

            #endregion

            #region Repositories

            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            #endregion

            #region Services

            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPaymentValidationService, PaymentValidationService>();
            services.AddScoped<ICardValidationService, CardValidationService>();
            services.AddScoped<IBankService, BankService>();

            #endregion

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Gateway");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
