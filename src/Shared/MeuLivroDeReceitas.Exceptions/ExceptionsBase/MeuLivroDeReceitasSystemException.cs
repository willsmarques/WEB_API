using System.Runtime.Serialization;

namespace MeuLivroDeReceitas.Exceptions.ExceptionsBase;

[Serializable]
public class MeuLivroDeReceitasSystemException : SystemException
{
    public MeuLivroDeReceitasSystemException(string mensagem) : base(mensagem)
    {
    }

    protected MeuLivroDeReceitasSystemException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}
