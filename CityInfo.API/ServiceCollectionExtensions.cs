using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using System.Runtime.CompilerServices;

namespace CityInfo.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();  
            services.AddSingleton<CitiesDataStore>();
            services.AddSingleton<FileExtensionContentTypeProvider>();
            //services.AddScoped<IMailService,LocalMailService> ();
            //services.AddScoped<IMailService, CloudMailService>();
            return services;
        }
    }
}
