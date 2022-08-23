
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
        public bool CreateWebSiteCookieTypeDefinition([FromBody] WebSiteCookieTypeDefinitionCreateRequestDto definition)
        {
            return _engine.CreateWebSiteCookieTypeDefinition(definition);
        }

        [HttpPut()]
        public bool UpdateWebSiteCookieTypeDefinition([FromBody] WebSiteCookieTypeDefinitionUpdateRequestDto definition)
        {
            return _engine.UpdateWebSiteCookieTypeDefinition(definition);
        }

        [HttpDelete("{id}")]
        public bool DeleteWebSiteCookieTypeDefinition(int id)
        {
            return _engine.DeleteWebSiteCookieTypeDefinition(id);
        }
    }
}
