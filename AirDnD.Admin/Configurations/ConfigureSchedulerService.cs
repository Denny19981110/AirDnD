using Airdnd.Admin.Scheduler;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Airdnd.Admin.Configurations
{
    public static class ConfigureSchedulerService
    {
        public static IServiceCollection AddSchedulerServices(this IServiceCollection services)
        {
            services.AddScheduler();
            services.AddScoped<BlockTokenScheduler>();
            return services;
        }
    }
}