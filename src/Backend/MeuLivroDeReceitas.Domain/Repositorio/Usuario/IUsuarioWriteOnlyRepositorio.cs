using MeuLivroDeReceitas.Domain.Entidades;

namespace MeuLivroDeReceitas.Domain.Repositorio.Usuario;

public interface IUsuarioWriteOnlyRepositorio
{
    Task Adicionar(Entidades.Usuario usuario);
}
