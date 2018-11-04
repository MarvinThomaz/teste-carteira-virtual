using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Enums;
using teste_carteira_virtual.Middlewares;

namespace teste_carteira_virtual.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var validations = new List<ValidationResponse>
                {
                    new ValidationResponse
                    {
                        Type = ResponseType.UndefinedError
                    }
                };

                var response = new ObjectResponse<object>
                {
                    Validations = validations
                };

                var jsonReponse = JsonConvert.SerializeObject(response);

                context.Response.StatusCode = (int) ResponseType.UndefinedError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(jsonReponse);
            }
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}