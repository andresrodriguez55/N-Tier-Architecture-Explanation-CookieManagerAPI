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
            return _engine.GetWebSites(); ;
        }

        [HttpPost()]
        public ActionResult CreateWebSite([FromBody] WebSiteCreateRequestDto website)
        {
            if (_engine.CreateWebSite(website))
                return Ok();
            return BadRequest();
        }

        [HttpPut()]
        public ActionResult UpdateWebSite([FromBody] WebSiteUpdateRequestDto website)
        {
            if (_engine.UpdateWebSite(website))
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteWebSite(int id)
        {
            if (_engine.DeleteWebSite(id))
                return Ok();
            return BadRequest();
        }
    }
}
