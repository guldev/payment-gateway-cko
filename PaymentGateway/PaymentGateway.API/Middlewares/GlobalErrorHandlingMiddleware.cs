using Microsoft.AspNetCore.Http;
using PaymentGateway.Core.Constants;
using PaymentGateway.Core.Exceptions;
using PaymentGateway.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentGateway.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (BaseException ex)
            {
                _loggerService.LogError($"Business Exception with response code {ex.Message}");

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)ex.StatusCode;

                var gatewayResponse = new
                {
                    gatewayResponseCode = ex.Message,
                };

                var errorJson = JsonSerializer.Serialize(gatewayResponse);

                await response.WriteAsync(errorJson);
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Internal Server Error {ex.Message}");
                _loggerService.LogError($"Internal Server Error {ex.StackTrace}");

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var gatewayResponse = new
                {
                    gatewayResponseCode = GatewayResponseCode.SERVER_ERROR,
                    message = ex.Message
                };

                var errorJson = JsonSerializer.Serialize(gatewayResponse);

                await response.WriteAsync(errorJson);
            }
        }
    }
}
