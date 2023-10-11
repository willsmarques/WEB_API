﻿using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Comunicacao.Resposta;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace MeuLivroDeReceitas.API.Filtros;

public class UsuarioAutenticadoAtribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenControlle;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;

    public UsuarioAutenticadoAtribute(TokenController tokenControlle, IUsuarioReadOnlyRepositorio repositorio)
    {
        _tokenControlle = tokenControlle;
        _repositorio = repositorio;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        try
        {
            var token = TokenNaRequisicao(context);

            var usuario = _tokenControlle.RecuperarEmail(token);

            if (usuario is null)
            {
                throw new System.Exception();

            }

        }
        catch(SecurityTokenExpiredException)
        {
            TokenExpirado(context);

        }
        catch
        {
            UsuarioSemPermissao(context);

        }
    }

    private string TokenNaRequisicao(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(authorization))
        {
            throw new System.Exception();
        }

        return authorization["Bearer".Length..].Trim();
    }

    private void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.TOKEN_EXPIRADO)); 

            
    }

    private void UsuarioSemPermissao(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.USUARIO_SEM_PERMISSAO));


    }
}
