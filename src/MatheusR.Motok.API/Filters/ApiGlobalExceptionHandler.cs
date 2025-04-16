using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.CC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

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

        //if (_env.IsDevelopment())
        //    details.Extensions.Add("StackTrace", exception.StackTrace);

        //if (exception is EntityValidationException)
        //{
        //    details.Title = "One or more validation errors ocurred";
        //    details.Status = StatusCodes.Status422UnprocessableEntity;
        //    details.Type = "UnprocessableEntity";
        //    details.Detail = exception!.Message;
        //}
        //else if (exception is NotFoundException)
        //{
        //    details.Title = "Not Found";
        //    details.Status = StatusCodes.Status404NotFound;
        //    details.Type = "NotFound";
        //    details.Detail = exception!.Message;
        //}

        //else if (exception is RelatedAggregateException)
        //{
        //    details.Title = "Invalid Related Aggregate";
        //    details.Status = StatusCodes.Status422UnprocessableEntity;
        //    details.Type = "RelatedAggregate";
        //    details.Detail = exception!.Message;
        //}

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
        else
        {
            exceptionError.Mensagem = "Ocorreu um erro inesperado";
            statusCode = StatusCodes.Status500InternalServerError;

            //details.Title = "An unexpected error ocurred";
            //details.Status = StatusCodes.Status422UnprocessableEntity;
            //details.Type = "UnexpectedError";
            //details.Detail = exception.Message;
            _logger.LogError("An unhandled exception occurred: {Message}", exception.Message);
        }

        context.HttpContext.Response.StatusCode = (int)statusCode;
        context.Result = new ObjectResult(exceptionError);
        context.ExceptionHandled = true;
    }
}
