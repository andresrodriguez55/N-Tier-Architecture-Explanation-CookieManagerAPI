using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookiesSettings.Models;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.Cookies;
using KariyerNet.CookieManager.Business.Validations;
using KariyerNet.CookieManager.Business.Validations.Cookies;
using KariyerNet.CookieManager.Common.Exceptions;
using KariyerNet.CookieManager.Data.Contract.Repository;
using Mapster;

namespace KariyerNet.CookieManager.Business.BusinessEngine
{
    internal class CookieEngine : ICookieEngine
    {
        private readonly ICookieRepository _repository;
        private readonly IWebSiteCookieTypeDefinitionEngine _webSiteCookieTypeDefinitionEngine;

        public CookieEngine(ICookieRepository repository, IWebSiteCookieTypeDefinitionEngine webSiteCookieTypeDefinitionEngine)
        {
            _repository = repository;
            _webSiteCookieTypeDefinitionEngine = webSiteCookieTypeDefinitionEngine;
        }

        public bool CreateCookie(CookieCreateRequestDto request) 
        {
            ValidationTool.Validate<CookieCreateValidation>(request);

            var webSiteCookieDefinition = _webSiteCookieTypeDefinitionEngine.GetWebSiteCookieDefinitionById(request.WebSiteCookieTypeDefinitionId);
            if(webSiteCookieDefinition.IsRequired && !request.Status)
            {
                throw new BusinessException("Kabul edilmesi zorunludur.");
            }
             
            var entity = request.Adapt<Cookie>(); 
            _repository.Create(entity);
            return true;
        }

        public List<CookieListItemDto> GetCookies()
        {
            var data = _repository.GetList(null, null, null, c => c.WebSiteCookieTypeDefinition);
            return data.Select(d => new CookieListItemDto
            {
                CreatedDate = d.CreatedDate,
                Id = d.Id,
                SessionId = d.SessionId,
                Status = d.Status,
                WebSiteCookieTypeDefinitionId = d.WebSiteCookieTypeDefinitionId,
                WebSiteCookieTypeName = d.WebSiteCookieTypeDefinition.Title
            }).ToList();
        }
    }
}
