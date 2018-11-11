using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using teste_carteira_virtual.Domain.Abstractions;
using teste_carteira_virtual.Domain.Base;
using teste_carteira_virtual.Domain.Enums;
using teste_carteira_virtual.Middlewares;

namespace teste_carteira_virtual.Middlewares
{
    public class PagingParametersMiddleware
    {
        private readonly RequestDelegate _next;

        public PagingParametersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IPagingParametersAccessor accessor)
        {
            StringValues strPage;
            StringValues strRecordsPerPage;

            context.Request.Query.TryGetValue(nameof(strPage), out strPage);
            context.Request.Query.TryGetValue(nameof(strRecordsPerPage), out strRecordsPerPage);

            accessor.Page = await ConvertAndValidateNumber(strPage, context);
            accessor.RecordsPerPage = await ConvertAndValidateNumber(strRecordsPerPage, context);

            await _next(context);
        }

        private async Task<int> ConvertAndValidateNumber(string value, HttpContext context)
        {
            if(!string.IsNullOrEmpty(value))
            {
                int intValue;
                var result = Int32.TryParse(value.ToString(), out intValue);

                if(!result)
                {
                    await CreateBadRequestResponse(context);
                }

                return intValue;
            }

            return 0;
        }

        private async Task CreateBadRequestResponse(HttpContext context)
        {
            var validations = new List<ValidationResponse>
            {
                new ValidationResponse
                {
                    Type = ResponseType.BadRequestResult
                }
            };

            var response = new ObjectResponse<object>
            {
                Validations = validations
            };

            var jsonReponse = JsonConvert.SerializeObject(response);

            context.Response.StatusCode = (int) ResponseType.BadRequestResult;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(jsonReponse);
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class PagingParametersMiddlewareExtensions
    {
        public static void UsePagingParameters(this IApplicationBuilder app)
        {
            app.UseMiddleware<PagingParametersMiddleware>();
        }
    }
}