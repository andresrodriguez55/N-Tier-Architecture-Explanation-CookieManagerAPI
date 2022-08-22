using CookiesSettings.DAL;
using CookiesSettings.DTO;
using CookiesSettings.Models;
using Microsoft.AspNetCore.Mvc;

//Query Filter???
namespace CookiesSettings.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebsitesController : ControllerBase
    {
        private CookieSettingsContext dbContext;

        public WebsitesController(CookieSettingsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Websites> GetAllWebsites()
        {
            return this.dbContext.Websites.ToList();
        }

        [HttpGet("{id}")]
        public Websites GetWebsite(int id)
        {
            return this.dbContext.Websites.FirstOrDefault(website => website.Id.Equals(id));
        }

        /*
        [HttpGet("{name}")]
        public Websites GetByName(string name)
        {
            return this.dbContext.Websites.FirstOrDefault(website => website.Name.Equals(name));
        }
        */

        [HttpPost]
        public ActionResult PostWebsite([FromBody] WebsitesDTO websiteDto)
        {
            Websites website = new Websites();
            website.copyDTOData(websiteDto);

            try
            {
                this.dbContext.Websites.Add(website);
                this.dbContext.SaveChanges();
                return Ok("Website information saved!");
            }
            catch (Exception error)
            {
                return BadRequest(error.InnerException?.Message ?? error.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult PutWebsite(int id, [FromBody] WebsitesDTO websiteDto)
        {
            var searchedWebsite = this.dbContext.Websites.Where(website => website.Id.Equals(id)).FirstOrDefault();
            if(searchedWebsite == null)
                return BadRequest("Website not found...");

            searchedWebsite.copyDTOData(websiteDto);

            try 
            {
                this.dbContext.SaveChanges();
                return Ok("Website information updated!");
            }
            catch (Exception error)
            {
                return BadRequest(error.InnerException?.Message ?? error.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteWebsite(int id)
        {
            var searchedWebsite = this.dbContext.Websites.FirstOrDefault(website => website.Id.Equals(id));

            if (searchedWebsite == null)
                return BadRequest("Website not found...");

            this.dbContext.Websites.Remove(searchedWebsite);
            this.dbContext.SaveChanges();
            return Ok("Website deleted");
        }
    }
}