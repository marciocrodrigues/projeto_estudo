using MediatR;
using ProjetoParaEstudo.Application.ViewModel;
using System.Collections.Generic;

namespace ProjetoParaEstudo.Application.Queries
{
    public class ListarTodosUsuariosQuery : IRequest<List<UsuarioViewModel>>
    {
    }
}
