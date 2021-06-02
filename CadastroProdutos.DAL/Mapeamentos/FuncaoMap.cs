using CadastroProdutos.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProdutos.DAL.Mapeamentos
{
    public class FuncaoMap : IEntityTypeConfiguration<Funcao>
    {
        public void Configure(EntityTypeBuilder<Funcao> builder)
        {
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            builder.Property(f => f.Descricao).IsRequired().HasMaxLength(50);

            builder.HasData(new Funcao
            {

                Id = Guid.NewGuid().ToString(),
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR",
                Descricao = "Admin do sistema"

            },
            new Funcao
            {

                Id = Guid.NewGuid().ToString(),
                Name = "usuario",
                NormalizedName = "USUARIO",
                Descricao = "user do sistema"

            }

            );

            builder.ToTable("Funcoes");
            
        }
    }
}
