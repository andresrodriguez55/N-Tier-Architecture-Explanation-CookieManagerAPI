
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.WebSiteCookieTypeDefinitions;
using Microsoft.AspNetCore.Mvc;

namespace KariyerNet.CookieManagerApi.Controllers
{
    [ApiController]
    [Route("api/website-cookie-type-definitions")] 
    public class WebsiteCookieTypeDefinitionsController : ControllerBase
    {
        private readonly IWebSiteCookieTypeDefinitionEngine _engine;

        public WebsiteCookieTypeDefinitionsController(IWebSiteCookieTypeDefinitionEngine engine)
        {
            _engine = engine;
        }

        [HttpGet()]
        public List<WebSiteCookieTypeDefinitionListItemDto> GetWebSiteCookieTypeDefinitions()
        {
            return _engine.GetWebSiteCookieTypeDefinitions();
        }

        [HttpPost()]
        public ActionResult CreateWebSiteCookieTypeDefinition([FromBody] WebSiteCookieTypeDefinitionCreateRequestDto definition)
        {
            if (_engine.CreateWebSiteCookieTypeDefinition(definition))
                return Ok();
            return BadRequest();
        }

        [HttpPut()]
        public ActionResult UpdateWebSiteCookieTypeDefinition([FromBody] WebSiteCookieTypeDefinitionUpdateRequestDto definition)
        {
            if (_engine.UpdateWebSiteCookieTypeDefinition(definition))
                return Ok();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteWebSiteCookieTypeDefinition(int id)
        {
            if (_engine.DeleteWebSiteCookieTypeDefinition(id))
                return Ok();
            return BadRequest();
        }
    }
}
