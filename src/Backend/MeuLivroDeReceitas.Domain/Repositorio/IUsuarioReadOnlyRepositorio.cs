namespace MeuLivroDeReceitas.Domain.Repositorio;

public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
    Task<Entidades.Usuario> Login(string email, string senha);
    
}
