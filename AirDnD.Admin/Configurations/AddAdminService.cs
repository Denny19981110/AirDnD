using Airdnd.Core.Helper;

using Airdnd.Admin.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Airdnd.Admin.Filters;

namespace Airdnd.Admin.Configurations
{
    public static class AddAdminService
    {
        public static IServiceCollection AddAdminServices(this IServiceCollection services)
        {
            services.AddScoped<MemberService>();
            services.AddScoped<ChartService>();
            services.AddScoped<ListingManageService>();
            services.AddScoped<LoginService>();
            services.AddScoped<TransactionService>();
            services.AddScoped<CustomApiExceptionServiceFilter>();
            services.AddScoped<DemoShopAdminAuthorize>();



            return services;
        }
    }
}
