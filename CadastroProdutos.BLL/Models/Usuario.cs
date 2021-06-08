using CadastroProdutos.BLL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProdutos.BLL.Models
{
    public class Usuario: IdentityUser<string>
    {
        public string login { get; set; }
        public virtual ICollection<Produtos> produtos { get; set; }

    }
}
