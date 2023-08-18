using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace PublicAffairsPortal.WebUI.CustomAttributes
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationException)
            {
                var errorMessages = new List<ValidationFailure>();
                foreach (var error in validationException.Errors)
                {
                    errorMessages.Add(new ValidationFailure(error.PropertyName, error.ErrorMessage)); 
                }
                context.Result = new BadRequestObjectResult(new  Application.Common.Exceptions
                    .ValidationException(errorMessages));
                context.ExceptionHandled = true;
            }
        }

    }
}
