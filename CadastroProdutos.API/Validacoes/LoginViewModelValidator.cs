using CadastroProdutos.API.ViewModels;
using CadastroProdutos.BLL.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutos.API.Validacoes
{
    public class LoginViewModelValidator: AbstractValidator<LoginViewModel>
    {

        public LoginViewModelValidator()
        {
            RuleFor(p => p.Login)
               .NotNull().WithMessage("Preencha o login")
               .NotEmpty().WithMessage("Preencha o login");


            RuleFor(p => p.Senha)
               .NotNull().WithMessage("Preencha a senha")
               .NotEmpty().WithMessage("Preencha a senha");
              
        }
    }
}
