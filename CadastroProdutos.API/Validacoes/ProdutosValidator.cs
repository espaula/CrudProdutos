using CadastroProdutos.BLL;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutos.API.Controllers.Validacoes
{
    public class ProdutosValidator : AbstractValidator<Produtos>
    {
        public ProdutosValidator()
        {
            RuleFor(p => p.nomedoProduto)
               .NotNull().WithMessage("Preencha o nome do produto")
               .NotEmpty().WithMessage("Preencha o nome do produto")
               .MinimumLength(5).WithMessage("Use mais caracteres no nome do produto")
               .MaximumLength(50).WithMessage("Use menos caracteres no nome do produto");

            RuleFor(p => p.foto)
               .NotNull().WithMessage("Selecione um imagem")
               .NotEmpty().WithMessage("Preencha o imagem");
              
            RuleFor(x => x.valordeVenda)
                .NotNull()
                .WithMessage("Preencha o valor da venda")
                .GreaterThan(0)
                .WithMessage("valor da venda precisa ser maior que 0");
        }
    }
}
