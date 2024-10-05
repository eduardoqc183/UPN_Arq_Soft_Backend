using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command.Carrito
{
    public class PagarCarritoCommand : IRequest<bool>
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int FormaPagoId { get; set; }
        [Required]
        [Range(1, 3)]
        public int MonedaId { get; set; }
    }

    public class PagarCarritoEventHandler : IRequestHandler<PagarCarritoCommand, bool>
    {
        private readonly JorplastContext context;
        private readonly IMediator mediator;

        public PagarCarritoEventHandler(JorplastContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<bool> Handle(PagarCarritoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var carritoPendiente = await context.carritocompras
                    .Include(i => i.producto)
                    .Where(w => w.usuarioid == request.UserId && !w.completado)
                    .ToListAsync();

                if (carritoPendiente.Count > 0)
                {
                    var precioVenta = carritoPendiente.Sum(s => s.producto.preciosinigv);
                    var baseImponible = precioVenta / 1.18m;
                    var igv = precioVenta - baseImponible;

                    var pago = new pago
                    {
                        fecharegistro = DateTime.Now,
                        formapagoid = request.FormaPagoId,
                        monedaid = request.MonedaId,   
                        igv = igv,
                        porcentajeigv = 18,
                        valortotal = baseImponible,
                        total = precioVenta,
                        usuarioid = request.UserId
                    };

                    await context.pagos.AddAsync(pago);
                    var r = await context.SaveChangesAsync();

                    if (r > 0)
                    {
                        foreach (var item in carritoPendiente)
                        {
                            item.completado = true;
                            item.pago = pago;
                            await mediator.Publish(new EliminarDeCarritoEvento(item.carritocompraid), cancellationToken);
                        }

                        var rr = await context.SaveChangesAsync();
                        return rr > 0;
                    }
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
