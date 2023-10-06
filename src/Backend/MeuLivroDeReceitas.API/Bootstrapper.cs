using MeuLivroDeReceitas.API.Controllers;
using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Application.UseCase.Usuario.Registrar;
using MeuLivroDeReceitas.Application.UseCases.Login.FazerLogin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Collections.Specialized.BitVector32;

namespace MeuLivroDeReceitas.API;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {

        AdicionarChaveAdicionarSenha(services, configuration);
        AdcionarTokenJWT(services, configuration);

        services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();

    }

    private static void AdicionarChaveAdicionarSenha(IServiceCollection services,IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Configuracoes:ChaveAdicionalSenha");

        services.AddScoped(option => new EncrepitadorDeSenha(section.Value));

    }

    private static void AdcionarTokenJWT(IServiceCollection services, IConfiguration configuration)
    {
        var sectionTempoDeVida = configuration.GetRequiredSection("Configuracoes:TempoVidaToken");
        var sectionKey = configuration.GetRequiredSection("Configuracoes:ChaveToken");

        services.AddScoped(option => new TokenController(int.Parse(sectionTempoDeVida.Value), sectionKey.Value));

    }
}
