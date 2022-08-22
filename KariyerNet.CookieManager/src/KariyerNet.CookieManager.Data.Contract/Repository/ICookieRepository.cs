using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookiesSettings.Models;
using KariyerNet.CookieManager.Common.Data;

namespace KariyerNet.CookieManager.Data.Contract.Repository
{
    public interface ICookieRepository : IGenericRepository<Cookie, int>
    {

    }
}
