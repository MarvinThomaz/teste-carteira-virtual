using teste_carteira_virtual.Application.Commands;
using teste_carteira_virtual.Application.Queries;
using teste_carteira_virtual.Application.Services;
using teste_carteira_virtual.Domain.Commands;
using teste_carteira_virtual.Domain.Queries;
using teste_carteira_virtual.Domain.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppExtensions
    {
        public static void AddApp(this IServiceCollection services)
        {
            services.AddScoped<IAddCartCommand, AddCartCommand>();
            services.AddScoped<IDisableCartCommand, DisableCartCommand>();
            services.AddScoped<IUpdateCartValueCommand, UpdateCartValueCommand>();

            services.AddScoped<IGetActiveCartFromClientQuery, GetActiveCartFromClientQuery>();
            services.AddScoped<IGetActiveCartsQuery, GetActiveCartsQuery>();
            services.AddScoped<IGetCartFromExternalKeyQuery, GetCartFromExternalKeyQuery>();
            services.AddScoped<IGetClientFromDocumentIdQuery, GetClientFromDocumentIdQuery>();
            services.AddScoped<IGetClientFromPartOfNameQuery, GetClientFromPartOfNameQuery>();

            services.AddScoped<ICartApplicationService, CartApplicationService>();
            services.AddScoped<IClientApplicationService, ClientApplicationService>();
        }
    }
}