using Airdnd.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airdnd.Infrastructure
{
    public class Dependencies
    {
        public static void ConfigureServices( IConfiguration configuration, IServiceCollection services )
        {
            services.AddDbContext<AirBnBContext>(options => options.UseSqlServer(configuration.GetConnectionString("Airdnd")));
        }
    }
}
