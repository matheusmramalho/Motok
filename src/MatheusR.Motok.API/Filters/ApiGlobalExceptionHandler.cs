using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.CC.Models;
using MatheusR.Motok.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MatheusR.Motok.API.Filters;

public class ApiGlobalExceptionHandler : IExceptionFilter
{
    private readonly IHostEnvironment _env;
    private readonly ILogger<ApiGlobalExceptionHandler> _logger;

    public ApiGlobalExceptionHandler(IHostEnvironment env, ILogger<ApiGlobalExceptionHandler> logger)
    {
        _env = env;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var statusCode = 0;
        var exceptionError = new ApiResponseError();

        if (exception is MotokApplicationException)
        {
            exceptionError.Mensagem = exception.Message;
            statusCode = StatusCodes.Status400BadRequest;
        }
        else if (exception is MotokNotFoundException)
        {
            exceptionError.Mensagem = exception.Message;
            statusCode = StatusCodes.Status404NotFound;
        }
        else if (exception is DomainBusinessException)
        {
            exceptionError.Mensagem = exception.Message;
            statusCode = StatusCodes.Status400BadRequest;
        }
        else if (exception is MotokUnauthorizedAccessException)
        {
            exceptionError.Mensagem = "Incorrect credentials";
            statusCode = StatusCodes.Status401Unauthorized;
        }
        else
        {
            exceptionError.Mensagem = "Ocorreu um erro inesperado";
            statusCode = StatusCodes.Status500InternalServerError;

            _logger.LogError("An unhandled exception occurred: {Message}", exception.Message);
        }

        context.HttpContext.Response.StatusCode = (int)statusCode;
        context.Result = new ObjectResult(exceptionError);
        context.ExceptionHandled = true;
    }
}
