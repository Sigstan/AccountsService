using AccountsService.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AccountsService.Api.Infrastructure.Exceptions;

public class DomainExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception.GetType() == typeof(DomainException))
        {
            var exception = context.Exception as DomainException;
            context.Result = new BadRequestObjectResult(new BadRequestResponse()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = exception.Message,
                ErrorCode = exception.ErrorCode.ToString()
            });
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }

    public class BadRequestResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
    }
}