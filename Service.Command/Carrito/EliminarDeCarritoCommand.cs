using Common.Exceptions;
using MediatR;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command.Carrito
{
    public class EliminarDeCarritoCommand : IRequest<bool>
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CarritoCompraId { get; set; }
    }

    public class EliminarDeCarritoEventHandler : IRequestHandler<EliminarDeCarritoCommand, bool>
    {
        private readonly JorplastContext context;
        private readonly IMediator mediator;

        public EliminarDeCarritoEventHandler(JorplastContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<bool> Handle(EliminarDeCarritoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var carrito = await context.carritocompras.FindAsync(request.CarritoCompraId);
                if (carrito != null)
                {
                    context.carritocompras.Remove(carrito);
                    var r = await context.SaveChangesAsync();
                    await mediator.Publish(new EliminarDeCarritoEvento(request.CarritoCompraId), cancellationToken);
                    return r > 0;
                }

                throw new NotFoundException("Id no encontrado");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
