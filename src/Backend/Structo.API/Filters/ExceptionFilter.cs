using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Structo.Communication.Responses;
using Structo.Exceptions;
using Structo.Exceptions.ExceptionsBase;
using System.Net;

namespace Structo.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is StructoException structoException)
                HandleProjectException(structoException, context);
            else
                ThrowUnknowException(context);
        }

        private static void HandleProjectException(StructoException structoException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)structoException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(structoException.GetErrorMessages()));
        }

        private static void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
        }
    }
}
