using teste_carteira_virtual.Domain.Repositories;
using teste_carteira_virtual.Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfra(this IServiceCollection services)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
        }
    }
}