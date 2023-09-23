using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;

namespace Utilitario.ParaOsTestes.Token;

public class TokenControllerBuilder
{
    public static TokenController Instacia()
    {
        return new TokenController(1000, "dHxXTnk6MSdVa3s1aGFGNFlgcWxycUYtKk5EeHN6NkEjWcKjXkc3a3k=");
    }
}
