using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KariyerNet.CookieManager.Data.Contract.Repository;
using KariyerNet.CookieManager.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace KariyerNet.CookieManager.Data.DataAccessRegistration
{
    public static class DataAccessRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {

            services.AddScoped<IWebSiteRepository, WebSiteRepository>();
            services.AddScoped<IWebSiteCookieTypeDefinitionRepository, WebSiteCookieTypeDefinitionRepository>();
            services.AddScoped<ICookieRepository, CookieRepository>();

            return services;
        }
    }
}
