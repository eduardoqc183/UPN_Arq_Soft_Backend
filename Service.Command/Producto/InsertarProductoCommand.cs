using Dto.Model.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command.Producto
{
    public class InsertarProductoCommand : productoDto, IRequest<int>
    {

    }

    public class InsertarProductoEventHandler : IRequestHandler<InsertarProductoCommand, int>
    {
        private readonly JorplastContext context;
        private readonly IMediator mediador;

        public InsertarProductoEventHandler(JorplastContext context, IMediator _mediador)
        {
            this.context = context;
            mediador = _mediador;
        }

        public async Task<int> Handle(InsertarProductoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var p = new producto
                {
                    nombre = request.nombre,
                    descripcion = request.descripcion,
                    preciosinigv = request.preciosinigv,
                    codigoproducto = await ObtenerCodigo(),
                    fecharegistro = DateTime.Now
                };

                context.productos.Add(p);
                await context.SaveChangesAsync(cancellationToken);

                //propagación del evento de inserción.
                await mediador.Publish(new InsertarProductoEvento(request), cancellationToken);

                return p.productoid;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<string> ObtenerCodigo()
        {
            var d = await context.productos.OrderByDescending(o => o.codigoproducto).FirstOrDefaultAsync();
            if (d == null)
                return "P0001";

            var max = int.Parse(d.codigoproducto.Substring(1, 4));
            return $"P{max:0000}";
        }
    }
}
