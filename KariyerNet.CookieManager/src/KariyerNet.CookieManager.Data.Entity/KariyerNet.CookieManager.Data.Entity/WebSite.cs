using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using KariyerNet.CookieManager.Common.Data;

namespace CookiesSettings.Models
{
    public class WebSite : BaseEntity<int>, IHasCreatedDateEntity
    {
        public WebSite() 
        {
            this.WebSiteCookieTypeDefinitions = new List<WebSiteCookieTypeDefinition>();
        }

        public string Name { get; set; }
        public virtual List<WebSiteCookieTypeDefinition> WebSiteCookieTypeDefinitions { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
