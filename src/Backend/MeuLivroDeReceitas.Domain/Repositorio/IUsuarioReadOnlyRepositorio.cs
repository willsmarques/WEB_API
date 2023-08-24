namespace MeuLivroDeReceitas.Domain.Repositorio;

public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
    
}
