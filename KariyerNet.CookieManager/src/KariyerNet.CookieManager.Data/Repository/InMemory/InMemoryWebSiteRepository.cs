using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookiesSettings.Models;
using KariyerNet.CookieManager.Data.Context;
using KariyerNet.CookieManager.Data.Contract.Repository;

namespace KariyerNet.CookieManager.Data.Repository.InMemory
{
    //internalll
    public class InMemoryWebSiteRepository //: IWebSiteRepository
    {
        private List<WebSite> _webSites;

        public InMemoryWebSiteRepository(CookieSettingsContext context)
        {
            _webSites = new List<WebSite>() 
            { 
                new WebSite
                {
                    Id = 1,
                    Name = "kariyer",
                    CreatedDate = DateTime.UtcNow,
                },
                new WebSite
                {
                    Id = 2,
                    Name = "coensio",
                    CreatedDate = DateTime.UtcNow,
                },
                new WebSite
                {
                    Id = 3,
                    Name = "iskolig",
                    CreatedDate = DateTime.UtcNow,
                }
            };
        }

        public WebSite GetById(int id)
        {
            return _webSites.FirstOrDefault(website => website.Id.Equals(id));
        }

        public List<WebSite> ListAll()
        {
            return _webSites;
        }

        public void Create(WebSite entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            _webSites.Add(entity);
        }

        public void Update(WebSite entity)
        {
            var entityToUpdate = _webSites.FirstOrDefault(website => website.Id.Equals(entity.Id));

            entityToUpdate.CreatedDate = DateTime.UtcNow;
            entityToUpdate.Name = entity.Name;
        }

        public void Delete(WebSite entity)
        {
            var entityToDelete = _webSites.FirstOrDefault(website => website.Id.Equals(entity.Id));
            _webSites.Remove(entityToDelete);
        }
    }
}
