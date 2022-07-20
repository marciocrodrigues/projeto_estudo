using ProjetoParaEstudo.Core.Data;
using ProjetoParaEstudo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        void Adicionar(User user);
        void Atualizar(User user);
        Task<User> ObterPorId(Guid id);
        Task<IEnumerable<User>> ObterTodos();

    }
}
