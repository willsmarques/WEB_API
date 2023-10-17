using MeuLivroDeReceitas.Application.Servicos.Criptografia;
using MeuLivroDeReceitas.Application.Servicos.Token;

namespace Utilitario.ParaOsTestes.Token;

public class TokenControllerBuilder
{
    public static TokenController Instacia()
    {
        return new TokenController(1000, "MGYpeD04OGNhdVZywqNdSCkqdUVka1tZUH5wb1RmRk8nbGFNYiRyTmI=");
    }


    public static TokenController TokenExpirado()
    {
        return new TokenController(0.0166667, "MGYpeD04OGNhdVZywqNdSCkqdUVka1tZUH5wb1RmRk8nbGFNYiRyTmI=");
    }
}
