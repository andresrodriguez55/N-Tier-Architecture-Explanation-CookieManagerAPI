using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KariyerNet.CookieManager.Business.Dto.WebSites;

namespace KariyerNet.CookieManager.Business.Contract.BusinessEngine
{
    public interface IWebSiteEngine
    {
        WebSiteListItemDto? GetWebSiteById(int id);
        List<WebSiteListItemDto> GetWebSites();
        bool CreateWebSite(WebSiteCreateRequestDto request);
        bool UpdateWebSite(WebSiteUpdateRequestDto request);
        bool DeleteWebSite(int id);
    }
}
