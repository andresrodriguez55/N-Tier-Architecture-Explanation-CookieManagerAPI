using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace KariyerNet.CookieManagerApi.Controllers
{
    [ApiController]
    [Route("api/cookies")]
    public class CookiesController : ControllerBase
    {
        private readonly ICookieEngine _engine;

        public CookiesController(ICookieEngine engine)
        {
            _engine = engine;
        }

        [HttpGet()]
        public List<CookieListItemDto> GetCookies()
        {
            return _engine.GetCookies();
        }


        [HttpPost()]
        public bool CreateCookie(CookieCreateRequestDto cookie) 
        {
            return _engine.CreateCookie(cookie);
        }

    }
}
