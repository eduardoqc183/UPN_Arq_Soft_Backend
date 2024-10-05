using Dto.Model.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Command.Carrito;
using Service.Query.Carrito;
using Service.Query.Producto;

namespace UPN_Arq_Soft_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : CustomControllerBase
    {
        private readonly ILogger<CarritoController> _logger;
        private readonly IMediator mediator;

        public CarritoController(ILogger<CarritoController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpPost("RegistrarACarrito")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult> ObtenerProductos(RegistrarACarritoCommand cmd)
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

        [HttpPost("EliminarDeCarrito")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult> EliminarDeCarritoEvento(EliminarDeCarritoCommand cmd)
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

        [HttpGet("ObtenerCarrito")]
        [ProducesResponseType(typeof(List<carritoconsultaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ObtenerCarrito(int userid)
        {
            try
            {
                var res = await mediator.Send(new ObtenerCarritoQuery(userid));
                return Ok(res);
            }
            catch (Exception ex)
            {
                return OnError(ex);
            }
        }

        [HttpPost("PagarCarrito")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult> PagarCarrito(PagarCarritoCommand cmd)
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

        [HttpGet("ObtenerComprados")]
        [ProducesResponseType(typeof(List<carritoconsultaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ObtenerComprados(int userid)
        {
            try
            {
                var res = await mediator.Send(new ObtenerCompradosQuery(userid));
                return Ok(res);
            }
            catch (Exception ex)
            {
                return OnError(ex);
            }
        }

        [HttpGet("ObtenerPagos")]
        [ProducesResponseType(typeof(List<pagoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ObtenerPagos(int userid)
        {
            try
            {
                var res = await mediator.Send(new ObtenerPagosQuery(userid));
                return Ok(res);
            }
            catch (Exception ex)
            {
                return OnError(ex);
            }
        }
    }
}
