using CookiesSettings.Models;
using KariyerNet.CookieManager.Common.Data;
using KariyerNet.CookieManager.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KariyerNet.CookieManager.Data.Context
{
    public class CookieSettingsContext : DbContext
    {
        public CookieSettingsContext(DbContextOptions<CookieSettingsContext> options) : base(options) 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

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

        public override int SaveChanges()
        {

            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified)).ToList();
            SetDefaultDateTimeValues(entries);
            var count = base.SaveChanges();
            foreach (var entry in entries) entry.State = EntityState.Detached;
            return count;


        }

        private void SetDefaultDateTimeValues(List<EntityEntry> entries)
        {
            if (entries.Count <= 0) return;
            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added && entityEntry.Entity is IHasCreatedDateEntity)
                {
                    var createdDate = (DateTime)entityEntry.Entity.GetType().GetProperties()
                        .FirstOrDefault(x => x.Name == nameof(IHasCreatedDateEntity.CreatedDate)).GetValue(entityEntry.Entity);
                    if (createdDate == default) ((IHasCreatedDateEntity)entityEntry.Entity).CreatedDate = DateTime.Now;

                }
            }
        }
    }
}
