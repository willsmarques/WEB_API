using MeuLivroDeReceitas.Application.UseCase.Login.FazerLogin;
using MeuLivroDeReceitas.Comunicacao.Requisicoes;
using MeuLivroDeReceitas.Comunicacao.Resposta;
using Microsoft.AspNetCore.Mvc;

namespace MeuLivroDeReceitas.API.Controllers
{
    public class LoginController : MeuLivroDeReceitasController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RespostaLoginJson), StatusCodes.Status200OK)]

        public async Task<IActionResult> Login(
            [FromServices] ILoginUseCase useCase,
            [FromBody] RequisicaoLoginJson requisicao)
        {
            var resposta = await useCase.Executar(requisicao);
            return Ok(resposta);
        }
    }
}
