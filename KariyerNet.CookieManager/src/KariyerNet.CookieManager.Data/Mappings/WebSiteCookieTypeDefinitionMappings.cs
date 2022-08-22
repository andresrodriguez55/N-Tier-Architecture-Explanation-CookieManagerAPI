using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookiesSettings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KariyerNet.CookieManager.Data.Mappings
{
    public class WebSiteCookieTypeDefinitionMappings : IEntityTypeConfiguration<WebSiteCookieTypeDefinition>
    {
        public void Configure(EntityTypeBuilder<WebSiteCookieTypeDefinition> builder)
        {
            builder.ToTable("WebSiteCookieTypeDefinitions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CookieType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(150);
            builder.Property(x => x.IsRequired).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            
            //foreign key anotation?
            builder.Property(x => x.WebSiteId).IsRequired();
        }
    }
}
