using Microsoft.EntityFrameworkCore;
using ProjetoParaEstudo.Core.Data;
using ProjetoParaEstudo.Domain.Entities;
using ProjetoParaEstudo.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(User user)
        {
            _context.Users.Add(user);
        }

        public void Atualizar(User user)
        {
            _context.Users.Update(user);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<User> ObterPorEmailEPassword(string email, string senha)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Senha == senha);
        }

        public async Task<User> ObterPorId(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> ObterTodos()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
    }
}
