using MeuLivroDeReceitas.Comunicacao.Resposta;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MeuLivroDeReceitas.API.Filtros;

public class FiltroDasExceptions : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MeuLivroDeReceitasExeception)
        {
            TratarMeuLivroDeReceitasException(context);
        }
        else
        {
            LancarErroDesconhecido(context);

        };

    }
    private void TratarMeuLivroDeReceitasException(ExceptionContext context)
    {
        if(context.Exception is ErroDeValidacaoException)
        {
            TratarErrosDeValidacaoException(context);
        }
        else if (context.Exception is LoginInvalidoException)
        {

        }
    }
    private void TratarErrosDeValidacaoException(ExceptionContext context)
    {
        var erroDeValidacaoException = context.Exception as ErroDeValidacaoException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Result = new ObjectResult(new RespostaErroJson(erroDeValidacaoException.MessagensDeErro));
    }

    private void TratarLoginExection(ExceptionContext context)
    {
        var erroLogin = context.Exception as LoginInvalidoException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new RespostaErroJson(erroLogin.Message));

    }
    private void LancarErroDesconhecido(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new RespostaErroJson(ResourceMensagensDeErro.ERRO_DESCONHECIDO));
    }
}
