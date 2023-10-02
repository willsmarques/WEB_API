namespace MeuLivroDeReceitas.Exceptions.ExceptionsBase;

public class LoginInvalidoException: MeuLivroDeReceitasExeception
{
    public LoginInvalidoException() : base(ResourceMensagensDeErro.LOGIN_INVALIDO) { }

}
