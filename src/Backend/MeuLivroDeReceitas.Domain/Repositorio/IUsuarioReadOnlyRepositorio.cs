﻿namespace MeuLivroDeReceitas.Domain.Repositorio;

public interface IUsuarioReadOnlyRepositorio
{
    Task<bool> ExisteUsuarioComEmail(string email);
    Task<Entidades.Usuario> RecuperarPorEmailSenha(string email, string senha);
    
}
