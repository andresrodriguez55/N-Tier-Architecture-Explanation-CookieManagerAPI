using CookiesSettings.Models;
using KariyerNet.CookieManager.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace KariyerNet.CookieManager.Data.Context
{
    public class CookieSettingsContext : DbContext
    {
        public CookieSettingsContext(DbContextOptions<CookieSettingsContext> options) : base(options) { }

        /***
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("CookieSettingsCon")));
        }
         ***/

        public DbSet<WebSite> WebSites { get; set; }
        public DbSet<WebSiteCookieTypeDefinition> WebSiteCookieTypeDefinitions { get; set; }
        public DbSet<Cookie> Cookies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CookieMappings());
            modelBuilder.ApplyConfiguration(new WebSiteMappings());
            modelBuilder.ApplyConfiguration(new WebSiteCookieTypeDefinitionMappings());

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
