using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KariyerNet.CookieManager.Business.Dto.WebSiteCookieTypeDefinitions
{
    public class WebSiteCookieTypeDefinitionCreateRequestDto
    {
        public string CookieType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; } 
        public bool IsActive { get; set; } 
        public int WebSiteId { get; set; }
    }
}
