using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace KariyerNet.CookieManager.Business.Validations
{
    public static class ValidationTool
    {
        public static void Validate<T>(object entity) where T: class,IValidator, new() //AbstractValidator<T> -> IValidator
        {
            var validator = new T(); 

            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if(!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}
