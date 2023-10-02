
using MeuLivroDeReceitas.Comunicacao.Requisicoes;
using MeuLivroDeReceitas.Comunicacao.Resposta;

namespace MeuLivroDeReceitas.Application.UseCase.Usuario.Registrar;

public interface IRegistrarUsuarioUseCase
{
    Task<RespostaUsuarioRegistradoJson> Executar(RequisicaoRegistrarUsuarioJson requisicao);
}
