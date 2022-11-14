using Airdnd.Core.Entities;
using Airdnd.Infrastructure.Data;
using Airdnd.Web.Configurations;
using Airdnd.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Airdnd.Web
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddControllersWithViews();
            services.AddDbContext<AirBnBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Airdnd")));
            services.AddTransient<IDbConnection>(op => new SqlConnection(Configuration.GetConnectionString("Airdnd")));
            services.AddCoreServices();
            services.AddWebServices();

            services.AddAutoMapper(typeof(Startup));
            
            
            //Line
            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AirDnd", Version = "v1" });
            });
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
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirDnd"));
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //�����ҦA���v
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}"
               );

                endpoints.MapControllerRoute(
                    name: "Room",
                    pattern: "Calendar/{action=calendar}/{id?}"
                );

                endpoints.MapControllerRoute(
                   name: "Host",
                   pattern: "Host/User{id=1}",
                   defaults: new { controller = "House", action = "Host" });
            
                endpoints.MapControllerRoute(
                  name: "House",
                  pattern: "/House/{name='�L�̻��ڬO���Ϊ��~���H'}",
                  defaults: new { controller = "House", action = "Index" });

                endpoints.MapControllerRoute(
                   name: "MemberData",
                   pattern: "personal-info",
                   defaults: new { controller = "Member", action = "Member" });

                endpoints.MapControllerRoute(
                   name: "SafetyData",
                   pattern: "login-and-security",
                   defaults: new { controller = "Member", action = "Safety" });

                //編輯房源詳情頁面
                endpoints.MapControllerRoute(
                    name: "RoomInfo/{id?}",
                    pattern: "RoomInfo/{id?}", new { controller = "SellerRoomInfo", action = "SellerRoomInfo" });

                //編輯房源照片頁面
                endpoints.MapControllerRoute(
                    name:"RoomImage/{id?}",
                    pattern: "RoomImage/{id?}", new { controller = "SellerRoomInfo", action = "RoomImagePage" });

            }
            );
           
        }
    }
}
