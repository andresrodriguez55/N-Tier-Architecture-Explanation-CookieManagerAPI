using CookiesSettings.DAL;
using CookiesSettings.DTO;
using CookiesSettings.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookiesSettings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteCookieTypeDefinitionsController : ControllerBase
    {
        private CookieSettingsContext dbContext;

        public WebsiteCookieTypeDefinitionsController(CookieSettingsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Pagination, search filters??
        [HttpGet]
        public IEnumerable<WebsiteCookieTypeDefinitions> Get()
        {
            return this.dbContext.WebsiteCookieTypeDefinitions.ToList();
        }

        [HttpGet("{id}")]
        public WebsiteCookieTypeDefinitions Get(int id)
        {
            return this.dbContext.WebsiteCookieTypeDefinitions.FirstOrDefault(websiteCookieTypeDefinition =>
                websiteCookieTypeDefinition.Id.Equals(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] WebsiteCookieTypeDefinitionsDTO websiteCookieTypeDefinitionDTO)
        {
            var website = this.dbContext.Websites.FirstOrDefault(website => website.Id.Equals(websiteCookieTypeDefinitionDTO.WebsiteId));
            if(website == null)
                return BadRequest("Website not found!");

            WebsiteCookieTypeDefinitions websiteCookieTypeDefinition = new WebsiteCookieTypeDefinitions();
            websiteCookieTypeDefinition.copyDTOData(websiteCookieTypeDefinitionDTO);

            try
            {
                this.dbContext.WebsiteCookieTypeDefinitions.Add(websiteCookieTypeDefinition);
                this.dbContext.SaveChanges();
                return Ok("Website cookie type definition information saved!");
            }
            catch (Exception error)
            {
                return BadRequest(error.InnerException?.Message ?? error.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] WebsiteCookieTypeDefinitionsDTO websiteCookieTypeDefinitionDTO)
        {
            var searchedWebsiteCookieTypeDefinition = this.dbContext.WebsiteCookieTypeDefinitions.FirstOrDefault(
                websiteCookieTypeDefinition => websiteCookieTypeDefinition.Id.Equals(id));
            if (searchedWebsiteCookieTypeDefinition == null)
                return BadRequest("Searched website cookie type definition not found...");

            searchedWebsiteCookieTypeDefinition.copyDTOData(websiteCookieTypeDefinitionDTO);

            try
            {
                this.dbContext.SaveChanges();
                return Ok("Website cookie type definition information updated!");
            }
            catch (Exception error)
            {
                return BadRequest(error.InnerException?.Message ?? error.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var searchedWebSiteCookieTypeDefinition = 
                this.dbContext.WebsiteCookieTypeDefinitions.FirstOrDefault(websiteCookieTypeDefinition => 
                websiteCookieTypeDefinition.Id.Equals(id));

            if (searchedWebSiteCookieTypeDefinition == null)
                return BadRequest("Website cookie type definition not found...");

            this.dbContext.WebsiteCookieTypeDefinitions.Remove(searchedWebSiteCookieTypeDefinition);
            this.dbContext.SaveChanges();
            return Ok("Website cookie type definition deleted");
        }
    }
}
