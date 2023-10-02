using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Application.UseCase.Login.FazerLogin;
using MeuLivroDeReceitas.Application.UseCase.Usuario.Registrar;


namespace MeuLivroDeReceitas.API;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {

        AdicionarChaveAdicionarSenha(services, configuration);
        AdicionarTokenJWT(services, configuration);
        AdicionarUseCase(services);
  
    }

    private static void AdicionarChaveAdicionarSenha(IServiceCollection services,IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Configuracoes:ChaveAdicionalSenha");

        services.AddScoped(option => new EncrepitadorDeSenha(section.Value));

    }

    private static void AdicionarTokenJWT(IServiceCollection services, IConfiguration configuration)
    {
        var sectionTempoDeVida = configuration.GetRequiredSection("Configuracoes:TempoVidaToken");
        var sectionKey = configuration.GetRequiredSection("Configuracoes:ChaveToken");

        services.AddScoped(option => new TokenController(int.Parse(sectionTempoDeVida.Value), sectionKey.Value));

    }

    private static void AdicionarUseCase(IServiceCollection services)
    {
        services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>()
            .AddScoped<ILoginUseCase, LoginUseCase>();
    }
}
