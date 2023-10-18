﻿using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;
using MeuLivroDeReceitas.Application.Servicos.UsuarioLogado;
using MeuLivroDeReceitas.Application.UseCase.Usuario.AlterarSenha;
using MeuLivroDeReceitas.Application.UseCase.Usuario.Registrar;
using MeuLivroDeReceitas.Application.UseCases.Login.FazerLogin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeuLivroDeReceitas.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AdicionarChaveAdicionalSenha(services, configuration);
        AdicionarTokenJWT(services, configuration);
        AdicionarUseCases(services);
        AdicionarUsuarioLogado(services);
    }

    private static void AdicionarUsuarioLogado(IServiceCollection services)
    {
       services.AddScoped<IUsuarioLogado, UsuarioLogado>();
    }


    
    private static void AdicionarChaveAdicionalSenha(IServiceCollection services, IConfiguration configuration)
     {

        var section = configuration.GetRequiredSection("Configuracoes:Senha:ChaveAdicionalSenha");

        services.AddScoped(option => new EncrepitadorDeSenha(section.Value));
     }

     private static void AdicionarTokenJWT(IServiceCollection services, IConfiguration configuration)
     {
        var sectionTempoDeVida = configuration.GetRequiredSection("Configuracoes:jwt:TempoVidaTokenMinutos");
        var sectionKey = configuration.GetRequiredSection("Configuracoes:jwt:ChaveToken");
        services.AddScoped(option => new TokenController(int.Parse(sectionTempoDeVida.Value), sectionKey.Value));
     }


    private static void AdicionarUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegistrarUsuarioUseCase, RegistrarUsuarioUseCase>()
            .AddScoped<ILoginUseCase, LoginUseCase>()
            .AddScoped<IAlterarSenhaUseCase, AlterarSenhaUseCase>();
    }
}