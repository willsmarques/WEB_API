using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Infrastructure;
using MeuLivroDeReceitas.Infrastructure.Migrations;

namespace MeuLivroDeReceitas.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddRespositorio(builder.Configuration);

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

            app.Run();

            void AtualizarBaseDados()
            {
               var conexao      = builder.Configuration.GetConexao();
               var nomeDatabase = builder.Configuration.GetNomeDatabase();
              
               Database.CriarDataBase(conexao, nomeDatabase);

               app.MigrateBancoDeDados();

            }
        }
    }
}