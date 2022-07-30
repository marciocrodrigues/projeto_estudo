using System;

namespace ProjetoParaEstudo.Application.ViewModel
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel(string nome, string email, string senha, bool ativo)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Ativo = ativo;
        }

        public string Nome { get; private set; }

        public string Email { get; private set; }
        public string Senha { get; private set; }

        public bool Ativo { get; private set; }
    }
}
