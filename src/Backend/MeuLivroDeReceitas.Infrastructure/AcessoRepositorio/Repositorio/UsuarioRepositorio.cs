using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio
{
    internal class UsuarioRepositorio : IUsuarioWriteOnlyRepositorio,IUsuarioReadOnlyRepositorio 
    {
        private readonly MeuLivroDeReceitaContext _context;

        public UsuarioRepositorio(MeuLivroDeReceitaContext contexto)
        {
            _context = contexto;

        }

        public async Task Adicionar(Usuario usuario)
        {
          await  _context.Usuarios.AddAsync(usuario);
        }

        public async Task<bool> ExisteUsuarioComEmail(string email)
        {
           return await _context.Usuarios.AnyAsync(c => c.Email.Equals(email));
        }
    }
}
