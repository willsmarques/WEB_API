﻿using MeuLivroDeReceitas.Domain.Repositorio;
using Moq;

namespace Utilitario.ParaOsTestes.Repositorios;

public class UsuarioReadOnlyRepositorioBuilder
{
    private static UsuarioReadOnlyRepositorioBuilder _instance;
    private readonly Mock<IUsuarioReadOnlyRepositorio> _repositorio;

    private UsuarioReadOnlyRepositorioBuilder()
    {
        if (_repositorio == null)
        {
            _repositorio = new Mock<IUsuarioReadOnlyRepositorio>();
        }
    }

    public UsuarioReadOnlyRepositorioBuilder ExisteUsuarioComEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
            _repositorio.Setup(i => i.ExisteUsuarioComEmail(email)).ReturnsAsync(true);
        
        return this;
    }

    public static UsuarioReadOnlyRepositorioBuilder Instancia()
    {
        _instance = new UsuarioReadOnlyRepositorioBuilder();
        return _instance;

    }

    public IUsuarioReadOnlyRepositorio Construir()
    {
        return _repositorio.Object;
    }
}
