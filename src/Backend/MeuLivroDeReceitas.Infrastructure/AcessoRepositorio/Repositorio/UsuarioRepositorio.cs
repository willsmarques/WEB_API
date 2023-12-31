﻿using MeuLivroDeReceitas.Domain.Entidades;
using MeuLivroDeReceitas.Domain.Repositorio.Usuario;
using Microsoft.EntityFrameworkCore;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepositorio.Repositorio
{
    internal class UsuarioRepositorio : IUsuarioWriteOnlyRepositorio,IUsuarioReadOnlyRepositorio, IUsuarioUpdateOnlyRepositorio
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

        public async Task<Usuario> RecuperarPorEmail(string email)
        {
            return await _context.Usuarios.AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Email.Equals(email));
            
        }

        public async Task<Usuario> RecuperarPorEmailSenha(string email, string senha)
        {
            return await _context.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email.Equals(email) && c.Senha.Equals(senha));
        }

        public async Task<Usuario> RecuperarPorId(long id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        void IUsuarioWriteOnlyRepositorio.RecuperarPorId(long id)
        {
            throw new NotImplementedException();
        }
    }
}
