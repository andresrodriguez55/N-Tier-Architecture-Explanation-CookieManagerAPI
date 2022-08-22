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
    //internal
    internal class CookieRepository : GenericRepository<Cookie, int>, ICookieRepository
    {
        private readonly CookieSettingsContext _context;

        public CookieRepository(CookieSettingsContext context) : base(context)
        {

        }
    }
}
