using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProjetoParaEstudo.Core.Messages;

namespace ProjetoParaEstudo.Application.Commands
{
    public class CriarUsuarioCommand : Command
    {
        public CriarUsuarioCommand(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new CriarUsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class CriarUsuarioValidation : AbstractValidator<CriarUsuarioCommand>
        {
            public CriarUsuarioValidation()
            {
                RuleFor(p => p.Nome)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome é obrigatorio");

                RuleFor(p => p.Email)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("E-mail é obrigatorio")
                    .EmailAddress()
                    .WithMessage("Precisa ser um e-mail valido");
            }
        }
    }
}
