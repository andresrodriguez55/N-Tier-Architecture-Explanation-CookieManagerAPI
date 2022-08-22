using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerNet.CookieManager.Business.Dto.Cookies
{
    public class CookieCreateRequestDto
    {
        public string SessionId { get; set; }
        public bool Status { get; set; }
        public int WebSiteCookieTypeDefinitionId { get; set; }
    }
}
