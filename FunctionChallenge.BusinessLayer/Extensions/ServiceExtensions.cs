using System;
using System.Collections.Generic;
using System.Text;
using FunctionChallenge.DataAccessLayer.Extensions;
using FunctionChallenge.BusinessLayer.Interfaces;
using FunctionChallenge.BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using AutoMapper;

namespace FunctionChallenge.BusinessLayer.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddBLDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IChartService, ChartService>();
            services.AddDatabaseDependencies();
            services.AddDatabaseContext(configuration);
            return services;
        }
    }
}
