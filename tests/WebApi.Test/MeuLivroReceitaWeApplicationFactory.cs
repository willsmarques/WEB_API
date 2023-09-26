﻿
using MeuLivroDeReceitas.Infrastructure.AcessoRepositorio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class MeuLivroReceitaWeApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(service =>
            {
                var descritor = service.SingleOrDefault(d => d.ServiceType == typeof(MeuLivroDeReceitaContext));
                if (descritor != null)
                    service.Remove(descritor);

                var provider = service.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                service.AddDbContext<MeuLivroDeReceitaContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                var serviceProvider = service.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();
                var scopeService = scope.ServiceProvider;

                var database = scopeService.GetRequiredService<MeuLivroDeReceitaContext>();
            });
    }

}
