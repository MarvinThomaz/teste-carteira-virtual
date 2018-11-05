using teste_carteira_virtual.Accessors;
using teste_carteira_virtual.Application.Abstractions;
using teste_carteira_virtual.Domain.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiExtensions
    {
        public static void AddApi(this IServiceCollection services)
        {
            services.AddScoped<IModelStateAccessor, ModelStateAccessor>();
            services.AddScoped<IPagingParametersAccessor, PagingParametersAccessor>();
        }
    }
}