using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCase.Usuario.Registrar;
using MeuLivroDeReceitas.Exceptions;
using MeuLivroDeReceitas.Exceptions.ExceptionsBase;
using System.ComponentModel.DataAnnotations;
using Utilitario.ParaOsTestes.Criptografia;
using Utilitario.ParaOsTestes.Mapper;
using Utilitario.ParaOsTestes.Repositorios;
using Utilitario.ParaOsTestes.Requisicoes;
using Utilitario.ParaOsTestes.Token;
using Xunit;

namespace UseCase.Test.Usuario.Registrar;

public  class RegistrarUsuarioUseCaseTest
{
    [Fact]
    public async Task validar_sucesso()
    {
        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();

        var useCase = CriarUseCase();
        var resposta = await useCase.Executar(requisicao);

        resposta.Should().NotBeNull();
        resposta.Token.Should().NotBeNullOrWhiteSpace();

    }

    [Fact]
    public async Task validar_Erro_Email_Ja_Registrado()
    {
        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();

        var useCase = CriarUseCase(requisicao.Email);

        Func<Task> acao = async () => { await useCase.Executar(requisicao); };

        await acao.Should().ThrowAsync<ErroDeValidacaoException>()
             .Where(execption => execption.MessagensDeErro.Count == 1 && execption.MessagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_JA_REGISTRADO));

    }

    [Fact]
    public async Task validar_Erro_Email_Vazio()
    {
        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();
        requisicao.Email = string.Empty;

        var useCase = CriarUseCase();

        Func<Task> acao = async () => { await useCase.Executar(requisicao); };

        await acao.Should().ThrowAsync<ErroDeValidacaoException>()
             .Where(execption => execption.MessagensDeErro.Count == 1 && execption.MessagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_USUARIO_EMBRANCO));
        
    }

    private RegistrarUsuarioUseCase CriarUseCase(string email = "")
    {
        var mapper = MapperBuilder.Instancia();
        var repositorio = UsuarioWriteOnlyRepositorioBuilder.Instancia().Construir();
        var unidadeDeTrabalho = UnidadeDeTrabalhoBuilder.Instancia().Construir();
        var encriptador = EncriptadorDeSenhaBuilder.Instacia();
        var token = TokenControllerBuilder.Instacia();
        var repositorioReadOnly = UsuarioReadOnlyRepositorioBuilder.Instancia().ExisteUsuarioComEmail(email).Construir();

        return new RegistrarUsuarioUseCase(repositorio, mapper, unidadeDeTrabalho, encriptador, token, repositorioReadOnly);

    } 
}
