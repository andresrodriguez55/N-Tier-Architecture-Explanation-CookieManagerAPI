using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KariyerNet.CookieManager.Business.Dto.WebSiteCookieTypeDefinitions;

namespace KariyerNet.CookieManager.Business.Contract.BusinessEngine
{
    public interface IWebSiteCookieTypeDefinitionEngine
    {
        WebSiteCookieTypeDefinitionListItemDto? GetWebSiteCookieDefinitionById(int id);
        List<WebSiteCookieTypeDefinitionListItemDto> GetWebSiteCookieTypeDefinitions();
        bool CreateWebSiteCookieTypeDefinition(WebSiteCookieTypeDefinitionCreateRequestDto request);
        bool UpdateWebSiteCookieTypeDefinition(WebSiteCookieTypeDefinitionUpdateRequestDto request);
        bool DeleteWebSiteCookieTypeDefinition(int id);
    }
}
