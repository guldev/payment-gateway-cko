using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NLog;
using PaymentGateway.API.Middlewares;
using PaymentGateway.API.Services;
using PaymentGateway.Core.Settings;
using PaymentGateway.Data;
using PaymentGateway.Data.Repositories;
using PaymentGateway.Logging;
using PaymentGateway.Services;
using PaymentGateway.Services.Authentication;
using System;
using System.IO;
using System.Text;

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
            #region Authentication
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:5000",
                    ValidAudience = "http://localhost:5000",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("cko-gateway-key@345"))
                };
            });
            #endregion

            #region DB Context
            services.AddDbContext<GatewayContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region Settings

            services.Configure<AcquiringBankSettings>(Configuration.GetSection("AcquiringBank"));

            #endregion

            #region Repositories

            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            #endregion

            #region Services

            services.AddSingleton<ITokenService, TokenService>();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
