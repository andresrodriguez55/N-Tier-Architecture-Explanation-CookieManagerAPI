using CookiesSettings.Models;
using KariyerNet.CookieManager.Business.Contract.BusinessEngine;
using KariyerNet.CookieManager.Business.Dto.WebSiteCookieTypeDefinitions;
using KariyerNet.CookieManager.Business.Validations;
using KariyerNet.CookieManager.Business.Validations.WebSiteCookieTypeDefinitions;
using KariyerNet.CookieManager.Common.Exceptions;
using KariyerNet.CookieManager.Data.Contract.Repository;
using Mapster;

namespace KariyerNet.CookieManager.Business.BusinessEngine
{
    internal class WebSiteCookieTypeDefinitionEngine : IWebSiteCookieTypeDefinitionEngine
    {
        private readonly IWebSiteCookieTypeDefinitionRepository _repository;

        public WebSiteCookieTypeDefinitionEngine(IWebSiteCookieTypeDefinitionRepository repository)
        {
            _repository = repository;
        }

        public WebSiteCookieTypeDefinitionListItemDto GetWebSiteCookieDefinitionById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new BusinessException("Id bulunamadı.");

            WebSiteCookieTypeDefinitionListItemDto response = entity.Adapt<WebSiteCookieTypeDefinitionListItemDto>();
            return response;
        }

        public List<WebSiteCookieTypeDefinitionListItemDto> GetWebSiteCookieTypeDefinitions()
        {
            var data = _repository.GetList();
            return data.Adapt<List<WebSiteCookieTypeDefinitionListItemDto>>();
        }

        public bool CreateWebSiteCookieTypeDefinition(WebSiteCookieTypeDefinitionCreateRequestDto request)
        {
            ValidationTool.Validate<WebSiteCookieTypeDefinitionCreateValidation>(request);

            var entity = request.Adapt<WebSiteCookieTypeDefinition>();
            _repository.Create(entity);
            return true;
        }

        public bool UpdateWebSiteCookieTypeDefinition(WebSiteCookieTypeDefinitionUpdateRequestDto request)
        {
            ValidationTool.Validate<WebSiteCookieTypeDefinitionUpdateValidation>(request);

            var entity = request.Adapt<WebSiteCookieTypeDefinition>();
            _repository.Update(entity);
            return true;
        }

        public bool DeleteWebSiteCookieTypeDefinition(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new BusinessException("Id bulunamadı.");

            _repository.Delete(entity);
            return true;
        }
    }
}
