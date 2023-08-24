﻿using FluentMigrator;

namespace MeuLivroDeReceitas.Infrastructure.Migrations.Versoes;

[Migration((long)NumeroVersoes.CriarTebelaUsuario, "Criar Tebela Usuario")]

public class Versao0000001 : Migration
{
    public override void Down()
    {
        
    }

    public override void Up()
    {
        var tabela = Create.Table("Usuario");
        
        tabela  
            .WithColumn("Nome").AsString(100).NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Senha").AsString(2000).NotNullable()
            .WithColumn("Telefone").AsString(14).NotNullable();
    }
}
