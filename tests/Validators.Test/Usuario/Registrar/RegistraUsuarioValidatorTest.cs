using FluentAssertions;
using MeuLivroDeReceitas.Application.UseCase.Usuario.Registrar;
using MeuLivroDeReceitas.Comunicacao.Requisicoes;
using MeuLivroDeReceitas.Exceptions;
using Xunit;
using Utilitario.ParaOsTestes.Requisicoes;

namespace Validators.Test.Usuario.Registrar;

public  class RegistraUsuarioValidatorTest
{
    [Fact]
    public void Validar_Sucesso()
    {
        var validator = new RegistrarUsuarioValidator();
        
        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeTrue();
        
    }

    [Fact]
    public void Validar_Erro_Email_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();
        requisicao.Email = string.Empty;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_EMBRANCO));

    }

    [Fact]
    public void Validar_Erro_Telefone_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();
        requisicao.Telefone = string.Empty;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_EMBRANCO));

    }

    [Fact]
    public void Validar_Erro_Senha_Vazio()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();
        requisicao.Senha = string.Empty;

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_EMBRANCO));

    }

    [Fact]
    public void Validar_Erro_Email_Invalidor()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();
        requisicao.Email = "we";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_USUARIO_INVALIDO));

    }

    [Fact]
    public void Validar_Erro_Telefone_Invalido()
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir();
        requisicao.Telefone = "314";

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.TELEFONE_USUARIO_INVALIDO));

    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Validar_Erro_Senha_Invalido(int tamanhoSenha)
    {
        var validator = new RegistrarUsuarioValidator();

        var requisicao = RequisicaoRegistraUsuarioBuilder.Contruir(tamanhoSenha);

        var resultado = validator.Validate(requisicao);

        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().ContainSingle().And.Contain(error => error.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_USUARIO_MINIMO_SEIS_CARACTERES));

    }

}
