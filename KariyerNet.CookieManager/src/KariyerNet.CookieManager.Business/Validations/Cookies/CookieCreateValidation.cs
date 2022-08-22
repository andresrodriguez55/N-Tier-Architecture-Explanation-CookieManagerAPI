using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using KariyerNet.CookieManager.Business.Dto.Cookies;

namespace KariyerNet.CookieManager.Business.Validations.Cookies
{
    internal class CookieCreateValidation : AbstractValidator<CookieCreateRequestDto>
    {
        public CookieCreateValidation()
        {
            RuleFor(x => x.SessionId).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
            RuleFor(x => x.WebSiteCookieTypeDefinitionId).NotEmpty();
        }
    }
}
