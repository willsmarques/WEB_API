using MeuLivroDeReceitas.API.Filtros;
using MeuLivroDeReceitas.API.Middleware;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Application.Servicos.Automapper;
using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Infrastructure;
using MeuLivroDeReceitas.Infrastructure.AcessoRepositorio;
using MeuLivroDeReceitas.Infrastructure.Migrations;


namespace MeuLivroDeReceitas.API;

public class Program
{
    public Program()
    {
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddRouting(option => option.LowercaseUrls = true);

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication(builder.Configuration);

        builder.Services.AddMvc(options => options.Filters.Add(typeof(FiltroDasExceptions)));

        builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
          {
              cfg.AddProfile(new AutoMapperConfiguracao());
          }).CreateMapper());


        builder.Services.AddScoped<UsuarioAutenticadoAttribute>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        AtualizarBaseDados();

        app.UseMiddleware<CultureMiddleware>();

        app.Run();

        void AtualizarBaseDados()
        {
            using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<MeuLivroDeReceitaContext>();

            bool? databaseInMemory = context?.Database?.ProviderName?.Equals("Microsoft.EntityFrameworkCore.InMemory");

            if (!databaseInMemory.HasValue || !databaseInMemory.Value)
            {
                var conexao = builder.Configuration.GetConexao();
                var nomeDatabase = builder.Configuration.GetNomeDatabase();

                Database.CriarDataBase(conexao, nomeDatabase);

                app.MigrateBancoDeDados();

            }



        }


    }
}





