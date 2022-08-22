using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using KariyerNet.CookieManager.Business.Dto.WebSites;

namespace KariyerNet.CookieManager.Business.Validations.WebSites
{
    internal class WebSiteCreateValidation : AbstractValidator<WebSiteCreateRequestDto>
    {
        public WebSiteCreateValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).Length(3, 100);
        }
    }
}
