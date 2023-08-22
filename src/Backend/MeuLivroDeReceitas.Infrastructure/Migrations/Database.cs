using Dapper;
using MySqlConnector;

namespace MeuLivroDeReceitas.Infrastructure.Migrations;

public static class Database
{
    public static void CriarDataBase(string conexaoComBancoDados, string nomeDatabase)
    {
     using var minhaConexao = new MySqlConnection(conexaoComBancoDados);
     
     var parametros = new DynamicParameters();
     parametros.Add("nome", nomeDatabase);

        var registro =    minhaConexao.Query("SELECT * FROM information_schema.SCHEMATA WHERE SCHEMA_NAME = @nome", parametros);
        if (!registro.Any())
        {
            minhaConexao.Execute($"CREATE DATABASE {nomeDatabase}");

        }   
    }
}
