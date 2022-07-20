using ProjetoParaEstudo.Core.Messages;
using ProjetoParaEstudo.Core.Messages.CommonMessages;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<bool> EnviarComando<T>(T comando) where T : Command;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    }
}
