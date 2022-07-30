
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjetoParaEstudo.Api.Extensions;
using ProjetoParaEstudo.Application.Commands;
using ProjetoParaEstudo.Application.InputModel;
using ProjetoParaEstudo.Application.Queries;
using ProjetoParaEstudo.Core.Communication.Mediator;
using ProjetoParaEstudo.Core.Messages.CommonMessages;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerCustomBase
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IMediator _mediator;

        public UserController(INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediatorHandler,
                              IMediator mediator,
                              ErrorsViewModel errors) : base(notifications, mediatorHandler, errors)
        {
            _mediatorHandler = mediatorHandler;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("criar-usuario")]
        public async Task<IActionResult> CriarUsuario([FromBody] UserInput input)
        {
            var command = new CriarUsuarioCommand(input.Nome, input.Email, input.Senha);

            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
            {
                return Ok("Usuário criado com sucesso");
            }

            var erros = await RetornarErros();

            return BadRequest(erros);
        }

        [HttpPost]
        [Route("listar-usuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var querie = new ListarTodosUsuariosQuery();

            var retorno = await _mediator.Send(querie);

            if (retorno.Any())
                return Ok(retorno);

            return NotFound();
        }

        [HttpPost]
        [Route("login-usuario")]
        public async Task<IActionResult> LoginUsuario([FromBody]UsuarioLoginInput input)
        {
            var command = new LoginUsuarioCommand(input.Email, input.Senha);

            var retorno = await _mediator.Send(command);

            if (OperacaoValida())
            {
                return Ok(retorno);
            }

            var erros = await RetornarErros();

            return BadRequest(erros);
        }
    }
}
