using Airdnd.Core.Interfaces;
using Airdnd.Infrastructure.Data;
using Airdnd.Infrastructure.Data.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Airdnd.Web.Configurations
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices( this IServiceCollection services )
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IWishlistQuery, WishlistQueryService>();
            return services;
        }
    }
}
