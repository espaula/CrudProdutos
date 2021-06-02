using CadastroProdutos.BLL;
using CadastroProdutos.BLL.Models;
using CadastroProdutos.DAL.Mapeamentos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroProdutos.DAL
{
    public class Contexto : IdentityDbContext<Usuario, Funcao, string>
    { 


        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Funcao> Funcoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes):base(opcoes)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new FuncaoMap());
            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new ProdutosMap());
        }

    }
}
