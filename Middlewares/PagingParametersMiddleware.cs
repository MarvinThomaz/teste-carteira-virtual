using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using teste_carteira_virtual.Domain.Abstractions;
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
            StringValues page;
            StringValues recordsPerPage;

            context.Request.Query.TryGetValue(nameof(page), out page);
            context.Request.Query.TryGetValue(nameof(recordsPerPage), out recordsPerPage);

            if(page.ToString() != null)
            {
                accessor.Page = Convert.ToInt32(page);
            }

            if(recordsPerPage.ToString() != null)
            {
                accessor.RecordsPerPage = Convert.ToInt32(recordsPerPage);
            }

            await _next(context);
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