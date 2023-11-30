using MeuLivroDeReceitas.Comunicacao.Requisicoes;
using MeuLivroDeReceitas.Comunicacao.Respostas;

namespace MeuLivroDeReceitas.Application.UseCase.Usuario.Registrar;

public interface IRegistrarUsuarioUseCase
{
    Task<RespostaUsuarioRegistradoJson> Executar(RequisicaoRegistrarUsuarioJson requisicao);
}
