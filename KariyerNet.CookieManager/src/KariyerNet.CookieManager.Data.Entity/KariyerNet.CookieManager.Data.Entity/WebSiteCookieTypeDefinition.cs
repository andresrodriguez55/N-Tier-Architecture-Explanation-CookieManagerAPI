using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KariyerNet.CookieManager.Common.Data;

namespace CookiesSettings.Models
{
    public class WebSiteCookieTypeDefinition : BaseEntity<int>, IHasCreatedDateEntity
    {
        public WebSiteCookieTypeDefinition() 
        {
            this.Cookies = new List<Cookie>();
        }

        public string CookieType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRequired { get; set; }
        public bool IsActive { get; set; }
        public int WebSiteId { get; set; }
        public virtual WebSite WebSite { get; set; } = null!;
        public virtual List<Cookie> Cookies { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
