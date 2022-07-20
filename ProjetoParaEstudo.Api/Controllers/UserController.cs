
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjetoParaEstudo.Api.Extensions;
using ProjetoParaEstudo.Api.InputModel;
using ProjetoParaEstudo.Application.Commands;
using ProjetoParaEstudo.Core.Communication.Mediator;
using ProjetoParaEstudo.Core.Messages.CommonMessages;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerCustomBase
    {
        private readonly IMediatorHandler _mediatorHandler;

        public UserController(INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediatorHandler,
                              ErrorsViewModel errors) : base(notifications, mediatorHandler, errors)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        [Route("criar-usuario")]
        public async Task<IActionResult> CriarUsuario([FromBody] UserInput input)
        {
            var command = new CriarUsuarioCommand(input.Nome, input.Email);

            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
            {
                return Ok("Usuário criado com sucesso");
            }

            var erros = await RetornarErros();

            return BadRequest(erros);
        }
    }
}
