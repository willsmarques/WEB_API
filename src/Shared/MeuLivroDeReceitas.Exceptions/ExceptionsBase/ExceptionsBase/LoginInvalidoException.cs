using System.Runtime.Serialization;

namespace MeuLivroDeReceitas.Exceptions.ExceptionsBase;

[Serializable]
public class LoginInvalidoException : MeuLivroDeReceitasExeception
{
    public LoginInvalidoException() : base(ResourceMensagensDeErro.LOGIN_INVALIDO)
    {
    }

}
