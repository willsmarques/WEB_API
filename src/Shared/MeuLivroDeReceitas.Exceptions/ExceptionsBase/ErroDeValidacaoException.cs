namespace MeuLivroDeReceitas.Exceptions.ExceptionsBase;

public  class ErroDeValidacaoException : MeuLivroDeReceitasExeception
{
     public List<string> MessagensDeErro {  get; set; }

    public ErroDeValidacaoException(List<string> messagensDeErro) : base(string.Empty)
    {
        MessagensDeErro = messagensDeErro;
    }
}
