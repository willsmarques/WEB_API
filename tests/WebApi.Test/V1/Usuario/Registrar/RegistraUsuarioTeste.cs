using FluentAssertions;
using MeuLivroDeReceitas.Exceptions;
using System.Net;
using System.Text.Json;
using Utilitario.ParaOsTestes.Requisicoes;
using Xunit;

namespace WebApi.Test.V1.Usuario.Registrar;

public class RegistraUsuarioTeste : ControllerBase
{
    private const string Metodo= "usuario";

    public RegistraUsuarioTeste(MeuLivroReceitaWeApplicationFactory<Program> factory) : base(factory)
    {

    }

    [Fact]
    public async Task Validar_sucesso()
    {
        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();
        var resposta = await PostRequest(Metodo, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaData = await JsonDocument.ParseAsync(responstaBody);

        respostaData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrWhiteSpace();
    }


    [Fact]
    public async Task Validar_Erro_Nome_Vazio()
    {
        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();
        requisicao.Nome = string.Empty;
        

        var resposta = await PostRequest(Metodo, requisicao);

        resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responstaBody = await resposta.Content.ReadAsStreamAsync();

        var respostaData = await JsonDocument.ParseAsync(responstaBody);

        var erro =  respostaData.RootElement.GetProperty("mensagens").EnumerateArray();

        erro.Should().ContainSingle().And.Contain(c => c.GetString().Equals(ResourceMensagensDeErro.NOME_USUARIO_EMBRANCO));
    }



}
