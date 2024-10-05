using Dto.Model.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Query.Carrito
{
    public class ObtenerCompradosQuery : IRequest<List<carritoconsultaDto>>
    {
        public int Userid { get; }

        public ObtenerCompradosQuery(int userid)
        {
            Userid = userid;
        }       
    }

    public class ObtenerCompradosEventHandler : IRequestHandler<ObtenerCompradosQuery, List<carritoconsultaDto>>
    {
        private readonly JorplastContext context;

        public ObtenerCompradosEventHandler(JorplastContext context)
        {
            this.context = context;
        }

        public async Task<List<carritoconsultaDto>> Handle(ObtenerCompradosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var comprados = await context.carritocompras
                    .Include(i => i.producto)
                    .Where(w => w.usuarioid == request.Userid && w.completado)
                    .Select(s => new carritoconsultaDto
                    {
                        CarritoId = s.carritocompraid,
                        Producto = s.producto.codigoproducto + " " + s.producto.nombre,                        
                        Precio = s.producto.preciosinigv,
                        Cantidad = s.cantidad,
                        FotoUrl = s.producto.imagenurl
                    })
                    .ToListAsync(cancellationToken: cancellationToken);

                return comprados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
