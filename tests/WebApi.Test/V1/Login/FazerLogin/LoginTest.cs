using FluentAssertions;
using MeuLivroDeReceitas.API;
using MeuLivroDeReceitas.Exceptions;
using System.Net;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.V1.Login.FazerLogin;

public class LoginTest : ControllerBase
{
    private const string Metodo = "login";

    private MeuLivroDeReceitas.Domain.Entidades.Usuario _usuario;
    private string _senha;

    public LoginTest(MeuLivroReceitaWeApplicationFactory<Program> factory) : base(factory)
    {
        _usuario = factory.RecuperarUsuario();
        _senha   = factory.RecuperarSenha();

    }

    [Fact]
    public async Task Validar_sucesso()
    {
        var requisicao = new MeuLivroDeReceitas.Comunicacao.Requisicoes.RequisicaoLoginJson
        {
            Email = _usuario.Email,
            Senha = _senha
        };

        var resposta = await PostRequest(Metodo, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.OK);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaData = await JsonDocument.ParseAsync(responstaBody);

        respostaData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_usuario.Nome);
        respostaData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Validar_Erro_Senha_Invalida()
    {
        var requisicao = new MeuLivroDeReceitas.Comunicacao.Requisicoes.RequisicaoLoginJson
        {
            Email = _usuario.Email,
            Senha = "senhaIvalidar"
        };

        var resposta = await PostRequest(Metodo, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaData = await JsonDocument.ParseAsync(responstaBody);

        var erros = respostaData.RootElement.GetProperty("mensagens").Deserialize<List<string>>();
        erros.Should().ContainSingle().And.Contain(ResourceMensagensDeErro.LOGIN_INVALIDO);
    }

    [Fact]
    public async Task Validar_Erro_Email_Invalido()
    {
        var requisicao = new MeuLivroDeReceitas.Comunicacao.Requisicoes.RequisicaoLoginJson
        {
            Email = "email@invalido.com",
            Senha = _senha
        };

        var resposta = await PostRequest(Metodo, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaData = await JsonDocument.ParseAsync(responstaBody);

        var erros = respostaData.RootElement.GetProperty("mensagens").Deserialize<List<string>>();
        erros.Should().ContainSingle().And.Contain(ResourceMensagensDeErro.LOGIN_INVALIDO);
    }

    [Fact]
    public async Task Validar_Erro_Email_Senha_Invalido()
    {
        var requisicao = new MeuLivroDeReceitas.Comunicacao.Requisicoes.RequisicaoLoginJson
        {
            Email = "email@invalido.com",
            Senha = "senhaIvalidar"
        };

        var resposta = await PostRequest(Metodo, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaData = await JsonDocument.ParseAsync(responstaBody);

        var erros = respostaData.RootElement.GetProperty("mensagens").Deserialize<List<string>>();
        erros.Should().ContainSingle().And.Contain(ResourceMensagensDeErro.LOGIN_INVALIDO);
    }

}
