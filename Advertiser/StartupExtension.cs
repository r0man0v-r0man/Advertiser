using AutoMapper;
using DataAccess;
using Domain;
using Domain.Interfaces;
using Domain.Interfaces.Implementation;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advertiser
{
    public static class StartupExtension
    {
        /// <summary>
        /// Подключение SPA директории
        /// </summary>
        /// <param name="services"></param>
        public static void AddSpa(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }
        public static void AddDbContext(this IServiceCollection serviceCollection,
                                string dataConnectionString = null)
        {
            serviceCollection.AddDbContext<AdvertiserContext>(options =>
                options.UseSqlServer(dataConnectionString ?? GetDataConnectionStringFromConfig()));
        }
        public static void AddRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddScoped<DataManager>();
            serviceCollection.AddScoped<ServiceManager>();
        }
        public static void AddTransient(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFlatService, FlatService>();
        }
        public static void AddAutoMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(typeof(MapProfile));
        }
        private static string GetDataConnectionStringFromConfig()
        {
            return new DbConfiguration().GetDataConnectionString();
        }
    }
}
