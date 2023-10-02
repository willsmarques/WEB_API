
using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Comunicacao.Requisicoes;
using MeuLivroDeReceitas.Comunicacao.Resposta;
using MeuLivroDeReceitas.Domain.Repositorio;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;

namespace MeuLivroDeReceitas.Application.UseCase.Login.FazerLogin;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUsuarioReadOnlyRepositorio _usuarioReadOnlyRepositorio;
    private readonly EncrepitadorDeSenha _encriptadorDeSenha;
    private readonly TokenController _tokenController;

    public LoginUseCase(IUsuarioReadOnlyRepositorio usuarioReadOnlyRepositorio, EncrepitadorDeSenha encriptadorDeSenha, TokenController tokenController)
    {
        _usuarioReadOnlyRepositorio = usuarioReadOnlyRepositorio;
        _encriptadorDeSenha = encriptadorDeSenha;
        _tokenController = tokenController;
    }

    public async Task<RespostaLoginJson> Executar(RequisicaoLoginJson request)
    {
        var senhaCriptografada = _encriptadorDeSenha.Criptografar(request.Senha);
        var usuario = await _usuarioReadOnlyRepositorio.RecuperarPorEmailSenha(request.email, senhaCriptografada);

        if (usuario == null)
        {
            throw new LoginInvalidoException();
        }

        return new RespostaLoginJson
        {
            Nome = usuario.Nome,
            Token = _tokenController.GerarToken(usuario.Email)
        };

    }
}
