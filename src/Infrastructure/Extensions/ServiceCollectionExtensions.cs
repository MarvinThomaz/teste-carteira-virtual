using teste_carteira_virtual.Domain.Abstractions;
using teste_carteira_virtual.Domain.Repositories;
using teste_carteira_virtual.Infrastructure.Context;
using teste_carteira_virtual.Infrastructure.Repositories;
using teste_carteira_virtual.Infrastructure.Transaction;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfra(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();

            services.AddScoped<ITransactionManager, EfTransactionManager>();

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
        }
    }
}