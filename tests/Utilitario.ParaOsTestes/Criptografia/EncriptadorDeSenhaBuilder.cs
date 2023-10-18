using MeuLivroDeReceitas.Application.Servicos.Criptografia;

namespace Utilitario.ParaOsTestes.Criptografia;

public class EncriptadorDeSenhaBuilder
{
    public static EncrepitadorDeSenha Instacia()
    {
        return new EncrepitadorDeSenha("ABCD123");
    }
}
