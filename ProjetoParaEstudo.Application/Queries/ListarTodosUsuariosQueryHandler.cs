using MediatR;
using ProjetoParaEstudo.Application.ViewModel;
using ProjetoParaEstudo.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Application.Queries
{
    public class ListarTodosUsuariosQueryHandler : IRequestHandler<ListarTodosUsuariosQuery, List<UsuarioViewModel>>
    {
        private readonly IUserRepository _userRepositopry;

        public ListarTodosUsuariosQueryHandler(IUserRepository userRepositopry)
        {
            _userRepositopry = userRepositopry;
        }

        public async Task<List<UsuarioViewModel>> Handle(ListarTodosUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _userRepositopry.ObterTodos();

            return usuarios.Select(x => new UsuarioViewModel(x.Nome, x.Email, x.Senha, x.Ativo)).ToList();
        }
    }
}
