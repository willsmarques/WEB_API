using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuLivroDeReceitas.Domain.Extension
{
    public static class RepositorioExtension
    {
        public static string GetNomeDatabase(this IConfiguration configurationManager)
        {
            var nomeDatabase = configurationManager.GetConnectionString("NomeDatabase");
            return nomeDatabase;
        }

        public static string GetConexao(this IConfiguration configurationManager)
        {
            var conexao = configurationManager.GetConnectionString("Conexao");
            return conexao;
        }

        public static string GetConexaoCompleta(this IConfiguration configurationManager)
        {
            var nomeDatabase = configurationManager.GetNomeDatabase();
            var conexao = configurationManager.GetConexao();

            return $"{conexao}Database={nomeDatabase}";
        }
    }
}
