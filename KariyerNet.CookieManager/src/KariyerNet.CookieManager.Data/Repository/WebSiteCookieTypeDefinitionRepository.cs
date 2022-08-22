using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookiesSettings.Models;
using KariyerNet.CookieManager.Common.Data;
using KariyerNet.CookieManager.Data.Context;
using KariyerNet.CookieManager.Data.Contract.Repository;

namespace KariyerNet.CookieManager.Data.Repository
{
    internal class WebSiteCookieTypeDefinitionRepository : 
        GenericRepository<WebSiteCookieTypeDefinition, int>, IWebSiteCookieTypeDefinitionRepository
    {
        private readonly CookieSettingsContext _context;

        public WebSiteCookieTypeDefinitionRepository(CookieSettingsContext context) : base(context)
        {

        }
    }
}
