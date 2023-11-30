using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Comunicacao.Respostas;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.API.Filtros;

public class UsuarioAutenticadoAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenControlle;
    private readonly IUsuarioReadOnlyRepositorio _repositorio;

    public UsuarioAutenticadoAttribute(TokenController tokenControlle, IUsuarioReadOnlyRepositorio repositorio)
    {
        _tokenControlle = tokenControlle;
        _repositorio = repositorio;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenNaRequisicao(context);

            var emailusuario = _tokenControlle.RecuperarEmail(token);

            var usuario = await _repositorio.RecuperarPorEmail(emailusuario);


            if (usuario is null)
            {
                throw new MeuLivroDeReceitasSystemException(string.Empty) ;

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

    private static string TokenNaRequisicao(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrWhiteSpace(authorization))
        {
            throw new MeuLivroDeReceitasSystemException(string.Empty);
        }

        return authorization["Bearer".Length..].Trim();
    }

    private static void TokenExpirado(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.TOKEN_EXPIRADO)); 

            
    }

    private static void UsuarioSemPermissao(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new RespostaErroJson(ResourceMensagensDeErro.USUARIO_SEM_PERMISSAO));


    }

    Task IAsyncAuthorizationFilter.OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        throw new System.NotImplementedException();
    }
}
