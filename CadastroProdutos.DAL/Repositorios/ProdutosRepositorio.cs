using CadastroProdutos.BLL;
using CadastroProdutos.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroProdutos.DAL.Repositorios
{
    public class ProdutosRepositorio : RepositorioGenerico<Produtos>, IProdutosRepositorio
    {
        private readonly Contexto _contexto;

        public ProdutosRepositorio(Contexto contexto) : base(contexto)
        {
            _contexto = contexto;

        }

        public new IQueryable<Produtos> PegarTodos()
        {
            try
            {
                return _contexto.Produtos.Include(u => u.usuario);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public new async Task<Produtos> PegarPeloId(int id)
        {
            try
            {
               var entity = await _contexto.Produtos.Include(u => u.usuario).FirstOrDefaultAsync(p => p.produtoId == id);
                
                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IQueryable<Produtos> FiltrarProdutos(string nomeProduto)
        {
            try
            {
                var entity = _contexto.Produtos.Include(u => u.usuario).Where(p => p.nomedoProduto.Contains(nomeProduto));

                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
