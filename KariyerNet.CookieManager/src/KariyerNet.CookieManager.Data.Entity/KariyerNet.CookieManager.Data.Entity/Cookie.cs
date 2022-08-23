using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using KariyerNet.CookieManager.Common.Data;

namespace CookiesSettings.Models
{ 
    public class Cookie : BaseEntity<int>,IHasCreatedDateEntity
    {
        public string SessionId { get; set; }
        public bool Status { get; set; }
        public int WebSiteCookieTypeDefinitionId { get; set; }
        public virtual WebSiteCookieTypeDefinition WebSiteCookieTypeDefinition { get; set; } = null!;
        public DateTime CreatedDate { get; set ; }
    }
}
