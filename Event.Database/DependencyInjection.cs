using Event.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<EventDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IEventDbContext>(provider =>
            provider.GetService<EventDbContext>());
            return services;
        }
    }
}
