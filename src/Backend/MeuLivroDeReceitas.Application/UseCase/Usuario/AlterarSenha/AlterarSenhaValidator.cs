using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Requisicoes;

namespace MeuLivroDeReceitas.Application.UseCase.Usuario.AlterarSenha;

public class AlterarSenhaValidator : AbstractValidator<RequisicaoAlterarSenhaJson>
{
    public AlterarSenhaValidator()
    {
        RuleFor(c => c.NovaSenha).SetValidator(new SenhaValidator());
    }
}
