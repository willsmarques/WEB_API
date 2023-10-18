using System.Runtime.Serialization;

namespace MeuLivroDeReceitas.Exceptions.ExceptionsBase;

[Serializable]
public class LoginInvalidoException : MeuLivroDeReceitasSystemException
{
    public LoginInvalidoException() : base(ResourceMensagensDeErro.LOGIN_INVALIDO)
    {
    }

    protected LoginInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}
