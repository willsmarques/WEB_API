using System.Security.Cryptography;
using System.Text;

namespace MeuLivroDeReceitas.Application.Servicos.Criptografia;

public  class EncrepitadorDeSenha
{
    private readonly string _chaveDeEncriptacao;

    public EncrepitadorDeSenha(string chaveEncriptacao)
    {
        _chaveDeEncriptacao = chaveEncriptacao;

    }

    public string Criptografar(string senha)
    {
        var senhaComChaveAdicional = $"{senha}{_chaveDeEncriptacao}";
       
        var bytes = Encoding.UTF8.GetBytes(senhaComChaveAdicional);
        var sha512 = SHA512.Create();
        byte[] hasBytes = sha512.ComputeHash(bytes);
        return StringByte(hasBytes);


    }

    private static string StringByte(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }

}
