using CadastroProdutos.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroProdutos.BLL
{
    public class Produtos
    {


        public int produtoId { get; set; }
        public string nomedoProduto { get; set; }
        public double valordeVenda { get; set; }
        public byte[] foto { get; set; }

        public string usuarioId { get; set; }
        public Usuario usuario { get; set; }


    }
}
