

using MeuLivroDeReceitas.Comunicacao.Requisicoes;
using MeuLivroDeReceitas.Comunicacao.Resposta;

namespace MeuLivroDeReceitas.Application.UseCase.Login.FazerLogin;

public  interface ILoginUseCase
{
    Task<RespostaLoginJson> Executar(RequisicaoLoginJson request);
}
