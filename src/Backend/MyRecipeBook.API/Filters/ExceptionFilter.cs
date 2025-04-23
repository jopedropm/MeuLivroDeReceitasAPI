using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System;
using System.Net;

namespace MyRecipeBook.API.Filters;

//As exceptions caem aqui para serem tratada
public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MyRecipeBookException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownException(context);
        }
    }

    //Erro conhecido
    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException)
        {
            //Convertendo Exception em ErrorOnValidation...
            var exception = context.Exception as ErrorOnValidationException;

            //Numero do erro "StatusCode" 
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //Mensagem do erro "Result"
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.ErrorsMessages));
        }
    }

    //Erro desconhecido
    private void ThrowUnknownException(ExceptionContext context)
    {
        //Numero do erro "StatusCode"
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //Mensagem do erro "Result"
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesExceptions.UNKNOWN_ERROR));
    }
}
