using Dto.Model.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Command.Usuario;
using Service.Query.Producto;
using Service.Query.Usuario;
using System.ComponentModel.DataAnnotations;

namespace UPN_Arq_Soft_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : CustomControllerBase
    {
        private readonly IMediator mediator;

        public UsuarioController(ILogger<UsuarioController> logger, IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost("AutenticarUsuario")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult> AutenticarUsuario(AutenticarUsuarioCommand cmd)
        {
            try
            {
                var res = await mediator.Send(cmd);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return OnError(ex);
            }
        }

        [HttpPost("RegistrarUsuario")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult> RegistrarUsuario(RegistrarUsuarioCommand cmd)
        {
            try
            {
                var res = await mediator.Send(cmd);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return OnError(ex);
            }
        }

        [HttpGet("ValidarEmailRegistrado")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult> ValidarEmailRegistrado([EmailAddress][Required(AllowEmptyStrings = false)] string email)
        {
            try
            {
                var res = await mediator.Send(new ValidarEmailRegistradoQuery(email));
                return Ok(res);
            }
            catch (Exception ex)
            {
                return OnError(ex);
            }
        }
    }
}
