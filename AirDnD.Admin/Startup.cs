using Airdnd.Admin.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Airdnd.Admin.Filters;
using Airdnd.Core.Interfaces;
using Airdnd.Infrastructure.Data;
using Coravel;
using Airdnd.Admin.Scheduler;
using isRock.LineBot.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

namespace Airdnd.Admin
{
    public class Startup
    {

        //cors
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
            
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            //Test
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
                options.ForwardedForHeaderName = "X-Forwarded-For-My-Custom-Header-Name";
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            //Test
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("https://localhost:5000/api/LineWebHookApi/LineWebHook");
                                  });
            });

            Infrastructure.Dependencies.ConfigureServices(Configuration, services);
            services.AddAdminServices();
            services.AddJwtServices(Configuration);
            services.AddSwaggerServices();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddSchedulerServices();

            services.AddControllersWithViews();
            services.AddTransient<IDbConnection>(op => new SqlConnection(Configuration.GetConnectionString("Airdnd")));
            //Redis
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("RedisConnectionString");
                options.InstanceName = "AirdndRedisCache";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {



            

            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
                //cors
                app.UseForwardedHeaders();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirDnd"));
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //cors
                app.UseForwardedHeaders();
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            //cors
            app.UseCors(MyAllowSpecificOrigins);
            app.UseCertificateForwarding();

            app.UseAuthentication();

            app.UseAuthorization();

            app.ApplicationServices.UseScheduler(scheduler =>
            {
                scheduler.Schedule<BlockTokenScheduler>().EveryFiveMinutes();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "Login", pattern: "Login", new { Controller = "Auth", Action = "Login" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=index}");
            });

        }
    }
}
