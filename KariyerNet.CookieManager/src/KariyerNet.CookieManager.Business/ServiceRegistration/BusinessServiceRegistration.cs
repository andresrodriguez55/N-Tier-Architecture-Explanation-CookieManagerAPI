using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KariyerNet.CookieManager.Business.BusinessEngine;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using Microsoft.Extensions.DependencyInjection;

namespace KariyerNet.CookieManager.Business.ServiceRegistration
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IWebSiteEngine, WebSiteEngine>();
            services.AddScoped<IWebSiteCookieTypeDefinitionEngine, WebSiteCookieTypeDefinitionEngine>();
            services.AddScoped<ICookieEngine, CookieEngine>();

            return services;
        }
    }
}
