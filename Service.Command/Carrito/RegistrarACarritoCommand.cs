using MediatR;
using Persistencia.Models;
using Service.Command.Producto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command.Carrito
{
    public class RegistrarACarritoCommand : IRequest<bool>
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UsuarioId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductoId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }
    }

    public class RegistrarACarritoEventHandler : IRequestHandler<RegistrarACarritoCommand, bool>
    {
        private readonly JorplastContext context;
        private readonly IMediator mediator;

        public RegistrarACarritoEventHandler(JorplastContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<bool> Handle(RegistrarACarritoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cc = new carritocompra
                {
                    cantidad = request.Cantidad,
                    fecharegistro = DateTime.Now,
                    productoid = request.ProductoId,
                    usuarioid = request.UsuarioId
                };

                var prod = await context.productos.FindAsync(request.ProductoId);
                if (prod != null)
                {
                    context.carritocompras.Add(cc);
                    var r = await context.SaveChangesAsync();
                    await mediator.Publish(new RegistrarACarritoEvento(cc.carritocompraid, cc.usuarioid, cc.productoid, cc.cantidad, prod.codigoproducto + " " + prod.nombre, prod.preciosinigv), cancellationToken);
                    return r > 0;
                }
                throw new Exception("Producto no encontrado");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
