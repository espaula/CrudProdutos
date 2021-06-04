using CadastroProdutos.BLL;
using CadastroProdutos.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroProdutos.DAL.Interfaces
{
    public interface IProdutosRepositorio : IRepositorioGenerico<Produtos>
    {
        new IQueryable<Produtos> PegarTodos();

        new Task<Produtos> PegarPeloId(int id );

        IQueryable<Produtos> FiltrarProdutos(string nomeProduto);


    }
}
