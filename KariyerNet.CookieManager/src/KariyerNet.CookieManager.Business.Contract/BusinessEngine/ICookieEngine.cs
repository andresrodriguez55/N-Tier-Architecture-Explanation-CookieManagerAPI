using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KariyerNet.CookieManager.Business.Dto.Cookies;

namespace KariyerNet.CookieManager.Business.Contract.BusinessEngine
{
    public interface ICookieEngine
    {
        //CookieListItemDto GetCookieById(int id);
        List<CookieListItemDto> GetCookies();
        bool CreateCookie(CookieCreateRequestDto request);
        //bool UpdateCookie(CookieUpdateRequestDto request);
        //bool DeleteCookie(int id);
    }
}
