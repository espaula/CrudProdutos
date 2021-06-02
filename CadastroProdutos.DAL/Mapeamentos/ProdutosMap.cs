using CadastroProdutos.BLL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroProdutos.DAL.Mapeamentos
{
    public class ProdutosMap : IEntityTypeConfiguration<Produtos>
    {
        public void Configure(EntityTypeBuilder<Produtos> builder)
        {
            builder.HasKey(p => p.produtoId);
            builder.Property(p => p.nomedoProduto).IsRequired().HasMaxLength(50);
            builder.Property(p => p.imagem).IsRequired().HasMaxLength(15);
            builder.Property(p => p.valordeVenda).IsRequired();

            builder.HasOne(g => g.usuario).WithMany(g => g.produtos).HasForeignKey(g => g.usuarioId).IsRequired();


            builder.ToTable("tb_produtos");


        }
    }
}
