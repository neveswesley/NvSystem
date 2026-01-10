using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NvSystem.Communications;
using NvSystem.Communications.Responses;
using NvSystem.Exceptions.ExceptionsBase;

namespace NvSystem.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NvSystemException)
            HandleProjectException(context);

        else
            ThrowUnknownException(context);
    }

    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException)
        {
            var exception = context.Exception as ErrorOnValidationException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.ErrorMessages));
        }
        
        if (context.Exception is EmailAlreadyExistsException)
        {
            var exception = context.Exception as EmailAlreadyExistsException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;

            context.Result = new ConflictObjectResult(new ResponseErrorJson(exception.Message));
        }
        
    }

    private void ThrowUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
    }
}