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
    public class ObtenerPagosQuery : IRequest<List<pagoDto>>
    {
        public int Userid { get; }
        public ObtenerPagosQuery(int userid)
        {
            Userid = userid;
        }
    }

    public class ObtenerPagosEventHandler: IRequestHandler<ObtenerPagosQuery, List<pagoDto>>
    {
        private readonly JorplastContext context;
        public ObtenerPagosEventHandler(JorplastContext context)
        {
            this.context = context;
        }

        public async Task<List<pagoDto>> Handle(ObtenerPagosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var pagos = await context.pagos
                    .Where(w => w.usuarioid == request.Userid)
                    .Select(s => new pagoDto
                    {
                        pagoid = s.pagoid,
                        monedaid = s.monedaid,
                        formapagoid = s.formapagoid,
                        valortotal = s.valortotal,
                        porcentajeigv = s.porcentajeigv,
                        igv = s.igv,
                        total = s.total,
                        fecharegistro = s.fecharegistro
                    })
                    .ToListAsync(cancellationToken: cancellationToken);

                return pagos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
