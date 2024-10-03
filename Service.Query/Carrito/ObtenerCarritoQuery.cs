using Dto.Model.Dtos;
using Dto.Model.MemoryDb;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Query.Carrito
{
    public class ObtenerCarritoQuery : IRequest<List<carritoconsultaDto>>
    {
        public int Userid { get; }
        public ObtenerCarritoQuery(int userid)
        {
            Userid = userid;
        }        
    }

    public class ObtenerCarritoEventHandler : IRequestHandler<ObtenerCarritoQuery, List<carritoconsultaDto>>
    {
        private readonly CarritoDictionary carritoDictionary;

        public ObtenerCarritoEventHandler(CarritoDictionary carritoDictionary)
        {
            this.carritoDictionary = carritoDictionary;
        }

        public Task<List<carritoconsultaDto>> Handle(ObtenerCarritoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.Run(() =>
                {
                    var carritos = carritoDictionary.Carrito.Values.Where(w => w.UsuarioId == request.Userid).AsQueryable();
                    
                    var res = carritos.ToList();
                    return res;
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
