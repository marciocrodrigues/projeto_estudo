using MediatR;
using ProjetoParaEstudo.Core.Messages;
using ProjetoParaEstudo.Core.Messages.CommonMessages;
using System;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> EnviarComando<T>(T comando) where T : Command
        {
            return await _mediator.Send(comando);
        }

        public Task PublicarEvento<T>(T evento) where T : Event
        {
            throw new NotImplementedException();
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            await _mediator.Publish(notificacao);
        }
    }
}
