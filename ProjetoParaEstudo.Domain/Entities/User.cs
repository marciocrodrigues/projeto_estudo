using FluentValidation;
using FluentValidation.Results;
using ProjetoParaEstudo.Core.DomainObjects;
using System;

namespace ProjetoParaEstudo.Domain.Entities
{
    public class User : Entity
    {
        public User() { }
        public User(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            DataCadastro = DateTime.Now;
            Ativo = true;
        }

        public string Nome { get; private set; }

        public string Email { get; private set; }
        public string Senha { get; private set; }

        public bool Ativo { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public ValidationResult ValidarSeAplicavel()
        {
            return new UserValidation().Validate(this);
        }

    }

    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(p => p.Nome)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome é obrigatorio");

            RuleFor(p => p.Senha)
                .NotEmpty()
                .NotNull()
                .WithMessage("Senha é obrigatoria");

            RuleFor(p => p.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("E-mail é obrigatorio")
                .EmailAddress()
                .WithMessage("Precisa ser um e-mail valido");
        }
    }
}
