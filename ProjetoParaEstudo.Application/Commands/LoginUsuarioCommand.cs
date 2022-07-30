using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProjetoParaEstudo.Application.ViewModel;

namespace ProjetoParaEstudo.Application.Commands
{
    public class LoginUsuarioCommand : IRequest<LoginUsuarioViewModel>
    {
        public LoginUsuarioCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public string Email { get; private set; }
        public string Senha { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public bool EhValido()
        {
            ValidationResult = new LoginUsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class LoginUsuarioValidation : AbstractValidator<LoginUsuarioCommand>
        {
            public LoginUsuarioValidation()
            {
                RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("Coloque um e-mail valido")
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome é obrigatorio");

                RuleFor(p => p.Senha)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("Senha ''r obrigatoria");
            }
        }
    }
}
