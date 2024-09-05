using Dto.Model.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Query.Producto;

namespace UPN_Arq_Soft_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : CustomControllerBase
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IMediator mediator;

        public ProductoController(ILogger<ProductoController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet("ObtenerProductos")]
        [ProducesResponseType(typeof(List<productoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> ObtenerProductos(string? nombreProducto, string? marca, decimal? precioMin, decimal? precioMax)
        {
            try
            {
                var data = new ObtenerProductosQuery(nombreProducto, marca, precioMin, precioMax);
                var res = await mediator.Send(data);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return OnError(ex);
            }
        }
    }
}
