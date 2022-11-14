using Airdnd.Core.Helper;
using Airdnd.Web.Controllers;
using Airdnd.Web.Interfaces;
using Airdnd.Web.Services;
using Airdnd.Web.Services.Cache;
using Airdnd.Web.Services.Partial;
using Airdnd.Web.WebApi;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace Airdnd.Web.Configurations
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices( this IServiceCollection services )
        {
            services.AddScoped<LoginService>();
            services.AddScoped<HostService>();
            services.AddScoped<UserAccountService>();
            services.AddScoped<WishlistService>();
            services.AddScoped<TripService>();
            services.AddScoped<BookingService>();
            services.AddScoped<TransactionService>();
            services.AddScoped<RoomSourceService>();
            services.AddScoped<ManaginRoomService>();
            services.AddScoped<HouseService>();
            services.AddScoped<WishlistPartialService>();
            services.AddScoped<HomeService>();
            services.AddScoped<IProductService, RedisProductService>();
            services.AddScoped<ProductService>();
            services.AddMemoryCache();
            services.AddScoped<GoogleMapService>();
            services.AddScoped<ISearchService, SearchDapperService>();
            services.AddScoped<PhotoService>();
            services.AddScoped<CalendarService>();
            services.AddScoped<OrderService>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login/Login";
            });
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddScoped<MailService>();
            services.AddScoped<HouseService>();
            services.AddScoped<IFilterPartialService, RedisFilterService>();
            services.AddScoped<FilterPartialService>();
            services.AddScoped<RankingService>();
            services.AddScoped<MemberService>();
            services.AddScoped<ExpenseService>();
            return services;
        }
    }
}
