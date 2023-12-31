﻿using FluentMigrator.Runner;
using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Domain.Repositorio;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using MeuLivroDeReceitas.Infrastructure.AcessoRepositorio;
using MeuLivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MeuLivroDeReceitas.Infrastructure;

public static class Bootstrapper
{
    public static void AddInfrastructure(this IServiceCollection services,IConfiguration configurationManager )
    {
        AddFluentMigrator(services, configurationManager);

        AddContexto(services, configurationManager);
        AddUnidadeDeTrabalho(services);
        AddRepositorios(services);
    }

    private static void AddContexto(IServiceCollection services, IConfiguration configurationManager)
    {
        _ = bool.TryParse(configurationManager.GetSection("Configuracoes:BancoDeDadosInMemory").Value, out bool BancoDeDadosInMemory);

        if (!BancoDeDadosInMemory)
        {
            var versaoServidor = new MySqlServerVersion(new Version(8, 0, 26));
            var connextionString = configurationManager.GetConexaoCompleta();
            services.AddDbContext<MeuLivroDeReceitaContext>(DbContextOptions =>
            {
                DbContextOptions.UseMySql(connextionString, versaoServidor);
            });

        }
    }

    private static void AddUnidadeDeTrabalho(IServiceCollection services)
    {
        services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();
    }

    private static void AddRepositorios(IServiceCollection services)
    {
        services.AddScoped<IUsuarioWriteOnlyRepositorio, UsuarioRepositorio>()
        .AddScoped<IUsuarioReadOnlyRepositorio, UsuarioRepositorio>()
        .AddScoped<IUsuarioUpdateOnlyRepositorio, UsuarioRepositorio>();
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager)
    {

        _ = bool.TryParse(configurationManager.GetSection("Configuracoes:BancoDeDadosInMemory").Value, out bool BancoDeDadosInMemory);

        if (!BancoDeDadosInMemory)
        {

                services.AddFluentMigratorCore().ConfigureRunner(c =>
            c.AddMySql5()
            .WithGlobalConnectionString(configurationManager.GetConexaoCompleta())
                .ScanIn(Assembly.Load("MeuLivroDeReceitas.Infrastructure"))
                .For.All());
        }
    }
}
