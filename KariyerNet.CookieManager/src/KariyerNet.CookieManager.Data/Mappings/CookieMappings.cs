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
    public class CookieMappings : IEntityTypeConfiguration<Cookie>
    {
        public void Configure(EntityTypeBuilder<Cookie> builder)
        {
            builder.ToTable("Cookies");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.SessionId).IsRequired().HasMaxLength(64);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            //foreign key annotation?
            builder.Property(x => x.WebSiteCookieTypeDefinitionId).IsRequired();
        }
    }
}
