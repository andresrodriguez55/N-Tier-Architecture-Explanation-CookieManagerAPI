using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using KariyerNet.CookieManager.Business.Dto.WebSiteCookieTypeDefinitions;

namespace KariyerNet.CookieManager.Business.Validations.WebSiteCookieTypeDefinitions
{
    internal class WebSiteCookieTypeDefinitionCreateValidation : AbstractValidator<WebSiteCookieTypeDefinitionCreateRequestDto>
    {
        public WebSiteCookieTypeDefinitionCreateValidation()
        {
            RuleFor(x => x.CookieType).NotEmpty();
            RuleFor(x => x.CookieType).Length(1, 50);
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Title).Length(3, 40);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Description).Length(6, 150);
            RuleFor(x => x.IsRequired);
            RuleFor(x => x.IsActive);
            RuleFor(x => x.WebSiteId).NotEmpty();
        }
    }
}
