using System.Runtime.Serialization;

namespace MeuLivroDeReceitas.Exceptions.ExceptionsBase;

[Serializable]

public class ErroDeValidacaoException : MeuLivroDeReceitasExeception
{
    public List<string> MessagensDeErro { get; set; }

    public ErroDeValidacaoException(List<string> messagensDeErro) : base(string.Empty)
    {
        MessagensDeErro = messagensDeErro;
    }

    protected ErroDeValidacaoException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
   

