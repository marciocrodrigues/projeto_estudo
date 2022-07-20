using MediatR;
using ProjetoParaEstudo.Core.Messages.CommonMessages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Api.Extensions
{
    public class ErrorsViewModel
    {
        private readonly DomainNotificationHandler _notifications;

        public ErrorsViewModel(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<List<Errors>> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notifications.ObterNotificacoes());

            var listErrors = new List<Errors>();

            notificacoes.ForEach(c => listErrors.Add(new Errors(c.Key, c.Value)));

            return listErrors;
        }
    }
}
