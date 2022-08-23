using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.WebSites;
using Microsoft.AspNetCore.Mvc;

namespace KariyerNet.CookieManagerApi.Controllers
{
    [ApiController]
    [Route("api/websites")]
    public class WebSitesController : ControllerBase
    {
        private readonly IWebSiteEngine _engine;

        public WebSitesController(IWebSiteEngine engine)
        {
            _engine = engine;
        }

        [HttpGet()]
        public List<WebSiteListItemDto> GetWebSites()
        {
            return _engine.GetWebSites();
        }

        [HttpPost()]
        public bool CreateWebSite([FromBody] WebSiteCreateRequestDto website)
        {
            return _engine.CreateWebSite(website);
        }

        [HttpPut()]
        public bool UpdateWebSite([FromBody] WebSiteUpdateRequestDto website)
        {
            return _engine.UpdateWebSite(website);
        }

        [HttpDelete("{id}")]
        public bool DeleteWebSite(int id)
        {
            return _engine.DeleteWebSite(id);
        }
    }
}
