using MediatR;
using ProjetoParaEstudo.Core.Communication.Mediator;
using ProjetoParaEstudo.Core.Interfaces;
using ProjetoParaEstudo.Core.Messages;
using ProjetoParaEstudo.Core.Messages.CommonMessages;
using ProjetoParaEstudo.Domain.Entities;
using ProjetoParaEstudo.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Application.Commands
{
    public class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserRepository _userRepositopry;
        private readonly IAutenticacaoService _autenticacaoService;

        public CriarUsuarioCommandHandler(IMediatorHandler mediatorHandler, IUserRepository userRepository, IAutenticacaoService autenticacaoService)
        {
            _mediatorHandler = mediatorHandler;
            _userRepositopry = userRepository;
            _autenticacaoService = autenticacaoService;
        }

        public async Task<bool> Handle(CriarUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var senhaEncriptada = _autenticacaoService.EmcriptarSha256Hash(message.Senha);

            var user = new User(message.Nome, message.Email, senhaEncriptada);

            var userValidation = user.ValidarSeAplicavel();

            if (!userValidation.IsValid)
            {
                foreach (var error in userValidation.Errors)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            _userRepositopry.Adicionar(user);

            return await _userRepositopry.UnitOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
