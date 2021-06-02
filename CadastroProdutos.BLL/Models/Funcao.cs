using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProdutos.BLL.Models
{
    public class Funcao: IdentityRole<String>
    {
        public string Descricao { get; set; }
    }
}
