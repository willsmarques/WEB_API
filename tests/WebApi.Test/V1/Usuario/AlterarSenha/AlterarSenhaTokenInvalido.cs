using FluentAssertions;
using MeuLivroDeReceitas.API;
using MeuLivroDeReceitas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilitario.ParaOsTestes.Requisicoes;
using Utilitario.ParaOsTestes.Token;
using Xunit;

namespace WebApi.Test.V1.Usuario.AlterarSenha;

public class AlterarSenhaTokenInvalido:  ControllerBase
{
    private const string Metodo = "usuario/alterar-senha";

    private MeuLivroDeReceitas.Domain.Entidades.Usuario _usuario;
    private string _senha;

    public AlterarSenhaTokenInvalido(MeuLivroReceitaWeApplicationFactory<Program> factory) : base(factory)
    {
        _usuario = factory.RecuperarUsuario();
        _senha = factory.RecuperarSenha();

    }


    [Fact]
    public async Task Validar_Erro_Token_Vazio()
    {
        var token = string.Empty;

        var requisicao = RequisicaoAlterarSenhaUsuarioBuilder.Construir();

        requisicao.SenhaAtual = _senha;

        var resposta = await PutRequest(Metodo, requisicao, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }

    [Fact]
    public async Task Validar_Erro_Usuario_Falso()
    {
        var token = TokenControllerBuilder.Instacia().GerarToken("usuario@falso.com");



        var requisicao = RequisicaoAlterarSenhaUsuarioBuilder.Construir();

        requisicao.SenhaAtual = _senha;

        var resposta = await PutRequest(Metodo, requisicao, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }


    [Fact]
    public async Task Validar_Erro_Token_Expirado()
    {
        var token = TokenControllerBuilder.TokenExpirado().GerarToken(_usuario.Email);

        Thread.Sleep(1000);

        var requisicao = RequisicaoAlterarSenhaUsuarioBuilder.Construir();

        requisicao.SenhaAtual = _senha;

        var resposta = await PutRequest(Metodo, requisicao, token);

        resposta.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

    }
}
