using MeuLivroDeReceitas.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace MeuLivroDeReceitas.Infrastructure.AcessoRepositorio;

public class MeuLivroDeReceitaContext : DbContext
{
   public DbSet<Usuario> Usuarios { get; set; }
    
    public MeuLivroDeReceitaContext(DbContextOptions<MeuLivroDeReceitaContext> option) : base(option){}



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuLivroDeReceitaContext).Assembly);
    }


}
