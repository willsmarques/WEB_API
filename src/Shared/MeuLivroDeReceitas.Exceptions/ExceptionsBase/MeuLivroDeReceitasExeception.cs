using System.Runtime.Serialization;

namespace MeuLivroDeReceitas.Exceptions.ExceptionsBase;

[Serializable]
public class MeuLivroDeReceitasExeception : SystemException
{
    public MeuLivroDeReceitasExeception(string mensagem) : base(mensagem)
    {
    }

    protected MeuLivroDeReceitasExeception(SerializationInfo info, StreamingContext context) : base(info, context) { }

}
