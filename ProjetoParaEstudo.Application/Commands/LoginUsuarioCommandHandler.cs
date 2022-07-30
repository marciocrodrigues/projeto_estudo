using MediatR;
using ProjetoParaEstudo.Application.ViewModel;
using ProjetoParaEstudo.Core.Communication.Mediator;
using ProjetoParaEstudo.Core.Interfaces;
using ProjetoParaEstudo.Core.Messages.CommonMessages;
using ProjetoParaEstudo.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Application.Commands
{
    public class LoginUsuarioCommandHandler : IRequestHandler<LoginUsuarioCommand, LoginUsuarioViewModel>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserRepository _userRepositopry;
        private readonly IAutenticacaoService _autenticacaoService;

        public LoginUsuarioCommandHandler(IUserRepository userRepositopry, IAutenticacaoService autenticacaoService, IMediatorHandler mediatorHandler)
        {
            _userRepositopry = userRepositopry;
            _autenticacaoService = autenticacaoService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<LoginUsuarioViewModel> Handle(LoginUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return null;

            var senhaEncriptada = _autenticacaoService.EmcriptarSha256Hash(message.Senha);

            var usuarioLogado = await _userRepositopry.ObterPorEmailEPassword(message.Email, senhaEncriptada);

            if (usuarioLogado == null)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification("erro", "Usuário não encontrado para os dados inseridos"));
                return null;
            }

            var token = _autenticacaoService.GerarJwtToken(usuarioLogado.Email, usuarioLogado.Id);

            return new LoginUsuarioViewModel(usuarioLogado.Email, token);
        }

        private bool ValidarComando(LoginUsuarioCommand message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }

            return false;
        }
    }
}
