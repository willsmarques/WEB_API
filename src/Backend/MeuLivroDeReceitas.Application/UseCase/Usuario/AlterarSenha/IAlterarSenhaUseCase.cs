using MeuLivroDeReceitas.Comunicacao.Requisicoes;

namespace MeuLivroDeReceitas.Application.UseCase.Usuario.AlterarSenha;

public interface IAlterarSenhaUseCase
{
    Task Executar(RequisicaoAlterarSenhaJson requisicao); 

}
