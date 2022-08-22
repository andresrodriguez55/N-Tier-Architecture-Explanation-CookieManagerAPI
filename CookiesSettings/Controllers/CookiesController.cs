using CookiesSettings.DAL;
using CookiesSettings.DTO;
using CookiesSettings.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookiesSettings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookiesController : ControllerBase
    {
        private CookieSettingsContext dbContext;

        public CookiesController(CookieSettingsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Cookies> Get()
        {
            return this.dbContext.Cookies.ToList();
        }

        /*
        [HttpGet("{id}")]
        public Cookies Get(int sessionid)
        {
            return this.dbContext.Cookies.FirstOrDefault(cookie => cookie.Id.Equals(id));
        }
        */

        [HttpGet("{sessionid}")]
        public Cookies Get(int sessionid) //birden fazla olabilir? unique check?
        {
            return this.dbContext.Cookies.FirstOrDefault(cookie => cookie.SessionId.Equals(sessionid));
        }

        [HttpPost]
        public ActionResult Post([FromBody] CookiesDTO cookieDTO)
        {
            var searchedWebsiteCookieTypeDefinition = this.dbContext.WebsiteCookieTypeDefinitions.FirstOrDefault(
                websiteCookieTypeDefinition => websiteCookieTypeDefinition.Id.Equals(cookieDTO.WebSiteCookieTypeDefinitionId));
            if(searchedWebsiteCookieTypeDefinition == null)
                return BadRequest("Website not found!");

            Cookies cookie = new Cookies();
            cookie.CopyDTOData(cookieDTO);
            bool isValid = cookie.isValidFor(searchedWebsiteCookieTypeDefinition); //DB triggeri mi olmalı?
            if (!isValid)
                return BadRequest("Required fields must be accepted!");

            try
            {
                this.dbContext.Cookies.Add(cookie);
                this.dbContext.SaveChanges();
                return Ok("Cookie information saved!");
            }
            catch (Exception error)
            {
                return BadRequest(error.InnerException?.Message ?? error.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CookiesDTO cookieDTO)
        {
            var searchedCookie = this.dbContext.Cookies.FirstOrDefault(cookie => cookie.Id.Equals(id));
            if (searchedCookie == null)
                return BadRequest("Searched cookie not found...");

            var searchedWebsiteCookieTypeDefinition = this.dbContext.WebsiteCookieTypeDefinitions.
                FirstOrDefault(websiteCookieTypeDefinition => 
                websiteCookieTypeDefinition.Id.Equals(cookieDTO.WebSiteCookieTypeDefinitionId));
            if (searchedWebsiteCookieTypeDefinition == null)
                return BadRequest("Website not found...");

            searchedCookie.CopyDTOData(cookieDTO);
            bool isValid = searchedCookie.isValidFor(searchedWebsiteCookieTypeDefinition); //DB triggeri mi olmalı?
            if (!isValid)
                return BadRequest("Required field must be accepted!");

            try
            {
                this.dbContext.SaveChanges();
                return Ok("Cookie information updated!");
            }
            catch (Exception error)
            {
                return BadRequest(error.InnerException?.Message ?? error.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var searchedCookie = this.dbContext.Cookies.FirstOrDefault(cookie => cookie.Id.Equals(id));

            if (searchedCookie == null)
                return BadRequest("Cookie not found...");

            this.dbContext.Cookies.Remove(searchedCookie);
            this.dbContext.SaveChanges();
            return Ok("Cookie deleted");
        }
    }
}
