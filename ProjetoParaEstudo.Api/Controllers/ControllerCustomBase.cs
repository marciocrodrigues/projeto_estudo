using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjetoParaEstudo.Api.Extensions;
using ProjetoParaEstudo.Core.Communication.Mediator;
using ProjetoParaEstudo.Core.Messages.CommonMessages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Api.Controllers
{
    public abstract class ControllerCustomBase : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly ErrorsViewModel _errors;
        private readonly IMediatorHandler _mediatorHandler;

        public ControllerCustomBase(INotificationHandler<DomainNotification> notifications,
                                    IMediatorHandler mediatorHandler,
                                    ErrorsViewModel errors)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
            _errors = errors;
        }

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }

        protected async Task<List<Errors>> RetornarErros()
        {
            return await _errors.InvokeAsync();
        }
    }
}
