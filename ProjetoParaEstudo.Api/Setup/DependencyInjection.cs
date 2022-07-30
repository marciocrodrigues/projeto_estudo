using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjetoParaEstudo.Api.Extensions;
using ProjetoParaEstudo.Application.Commands;
using ProjetoParaEstudo.Application.Queries;
using ProjetoParaEstudo.Application.ViewModel;
using ProjetoParaEstudo.Core.Communication.Mediator;
using ProjetoParaEstudo.Core.Interfaces;
using ProjetoParaEstudo.Core.Messages.CommonMessages;
using ProjetoParaEstudo.Core.Utils;
using ProjetoParaEstudo.Data.Repositories;
using ProjetoParaEstudo.Domain.Repositories;
using System.Collections.Generic;

namespace ProjetoParaEstudo.Api.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<ErrorsViewModel>();

            services.AddScoped<IAutenticacaoService, AutenticacaoService>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRequestHandler<CriarUsuarioCommand, bool>, CriarUsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<ListarTodosUsuariosQuery, List<UsuarioViewModel>>, ListarTodosUsuariosQueryHandler>();
            services.AddScoped<IRequestHandler<LoginUsuarioCommand, LoginUsuarioViewModel>, LoginUsuarioCommandHandler>();
        }
    }
}
