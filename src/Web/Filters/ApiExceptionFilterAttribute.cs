using System;
using System.Collections.Generic;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        
        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(BadRequestException), HandleBadRequestException },
            };
        }
        
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }
        
        
        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }
        
        private void HandleBadRequestException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = context.Exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            Console.WriteLine(context.Exception);
            
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
    
}