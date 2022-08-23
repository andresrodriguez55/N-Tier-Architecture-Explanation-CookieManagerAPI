using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookiesSettings.Models;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.WebSites;
using KariyerNet.CookieManager.Business.Validations;
using KariyerNet.CookieManager.Business.Validations.WebSites;
using KariyerNet.CookieManager.Data.Contract.Repository;
using Mapster;
using System.Runtime.CompilerServices;
using KariyerNet.CookieManager.Common.Exceptions;

[assembly: InternalsVisibleTo("NUnitTests")]
namespace KariyerNet.CookieManager.Business.BusinessEngine
{
    internal class WebSiteEngine : IWebSiteEngine
    {
        private readonly IWebSiteRepository _repository;
        public WebSiteEngine(IWebSiteRepository repository)
        {
            _repository = repository;
        }
        public WebSiteListItemDto GetWebSiteById(int id) 
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new BusinessException("Id bulunamadı.");

            var response = entity.Adapt<WebSiteListItemDto>();
            return response;
        }

        public List<WebSiteListItemDto> GetWebSites()
        {
            var data = _repository.GetList();
            return data.Adapt<List<WebSiteListItemDto>>();
        }

        public bool CreateWebSite(WebSiteCreateRequestDto request)
        {
            ValidationTool.Validate<WebSiteCreateValidation>(request);

            var entity = request.Adapt<WebSite>();
            _repository.Create(entity);
            return true;
        }

        public bool UpdateWebSite(WebSiteUpdateRequestDto request)
        {
            ValidationTool.Validate<WebSiteUpdateValidation>( request);

            var entity = request.Adapt<WebSite>();
            _repository.Update(entity);
            return true;
        }

        public bool DeleteWebSite(int id) //farklı metoda bağımlılık var
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new BusinessException("Id bulunamadı.");

            _repository.Delete(entity);
            return true;
        }
    }
}
