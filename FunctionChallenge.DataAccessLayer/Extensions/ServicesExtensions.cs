using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FunctionChallenge.DataAccessLayer.Repositories;
using FunctionChallenge.DataAccessLayer.EF;
using FunctionChallenge.DataAccessLayer.Interfaces;

namespace FunctionChallenge.DataAccessLayer.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services)
        {
            services.AddTransient<IChartRepository, ChartRepository>();
            services.AddTransient<IPointRepository,PointRepository>();
            services.AddTransient<IUserDataRepository,UserDataRepository>();
            services.AddTransient<IUnitOfWork, FCEFUnitOfWork>();
            return services;
        }
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<FCDbContext>(options => //???????
            {
                options.UseSqlServer(connection);
            });
            return services;
        }
    }
}
