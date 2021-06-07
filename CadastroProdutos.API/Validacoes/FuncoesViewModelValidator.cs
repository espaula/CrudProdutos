using CadastroProdutos.API.ViewModels;
using CadastroProdutos.BLL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutos.API.Validacoes
{
    public class FuncoesViewModelValidator: AbstractValidator<FuncoesViewModel>
    {

        public FuncoesViewModelValidator()
        {
            RuleFor(p => p.Name)
               .NotNull().WithMessage("Preencha o nome da função")
               .NotEmpty().WithMessage("Preencha o nome da função")
               .MinimumLength(1).WithMessage("Use mais caracteres no nome da função")
               .MaximumLength(50).WithMessage("Use menos caracteres no nome da função");

            RuleFor(p => p.Descricao)
               .NotNull().WithMessage("Preencha a descrição da função")
               .NotEmpty().WithMessage("Preencha a descrição da função")
               .MinimumLength(1).WithMessage("Use mais caracteres na descrição  da função")
               .MaximumLength(50).WithMessage("Use menos caracteres na descrição  da função");
          
        }
    }
}
