﻿using MeuLivroDeReceitas.Domain.Repositorio;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepositorio;

public sealed class UnidadeDeTrabalho : IDisposable, IUnidadeDeTrabalho
{
    private readonly MeuLivroDeReceitaContext _contexto;
    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
    }

    public UnidadeDeTrabalho(MeuLivroDeReceitaContext contexto)
    {
        _contexto = contexto;
    }

    public async Task Commit()
    {
        await _contexto.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _contexto.Dispose();

        }
        _disposed = true;
    }
}
