using CadastroProdutos.API.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutos.API.Validacoes
{
    public class RegistroViewModelValidator: AbstractValidator<RegistroViewModel>
    {

        public RegistroViewModelValidator()
        {
            RuleFor(p => p.NomeUsuario)
              .NotNull().WithMessage("Preencha o nome do usuario")
              .NotEmpty().WithMessage("Preencha o nome do usuario")
              .MinimumLength(1).WithMessage("Use mais caracteres no nome do usuario")
              .MaximumLength(50).WithMessage("Use menos caracteres no nome do usuario");

            RuleFor(p => p.CPF)
               .NotNull().WithMessage("Preencha o CPF")
               .NotEmpty().WithMessage("Preencha o CPF");
              
            RuleFor(p => p.Profissao)
             .NotNull().WithMessage("Preencha a profissão")
             .NotEmpty().WithMessage("Preencha a profissão")
             .MinimumLength(1).WithMessage("Use mais caracteres na profissão ")
             .MaximumLength(30).WithMessage("Use menos caracteres na profissão");


            RuleFor(p => p.Foto)
             .NotNull().WithMessage("Escolha a foto")
             .NotEmpty().WithMessage("Escolha a foto");
            
            RuleFor(p => p.Senha)
             .NotNull().WithMessage("Preencha a senha")
             .NotEmpty().WithMessage("Preencha a senha")
             .MinimumLength(6).WithMessage("Use mais caracteres na senha")
             .MaximumLength(50).WithMessage("Use menos caracteres na senha");

        }
    }
}
