using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjetoParaEstudo.Api.Extensions;
using ProjetoParaEstudo.Application.Commands;
using ProjetoParaEstudo.Core.Communication.Mediator;
using ProjetoParaEstudo.Core.Messages.CommonMessages;
using ProjetoParaEstudo.Data;
using ProjetoParaEstudo.Data.Repositories;
using ProjetoParaEstudo.Domain.Repositories;

namespace ProjetoParaEstudo.Api.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<ErrorsViewModel>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRequestHandler<CriarUsuarioCommand, bool>, CriarUsuarioCommandHandler>();
        }
    }
}
