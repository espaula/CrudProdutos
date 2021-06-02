using CadastroProdutos.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProdutos.DAL.Mapeamentos
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.CPF).IsRequired().HasMaxLength(20);
            builder.HasIndex(u => u.CPF).IsUnique();
            builder.Property(u => u.profissao).IsRequired().HasMaxLength(30);




            builder.ToTable("Usuarios");
        }
    }
}
