using Dto.Model.Dtos;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Assemblers
{
    public static class productoAssembler
    {
        public static productoDto ToDTO(this producto producto)
        {
            return new productoDto
            {
                productoid = producto.productoid,
                codigoproducto = producto.codigoproducto,
                descripcion = producto.descripcion,
                fecharegistro = producto.fecharegistro,
                imagenurl = producto.imagenurl,
                marca = producto.marca,
                monedaid = producto.monedaid,
                nombre = producto.nombre,
                preciosinigv = producto.preciosinigv,
                vendedor = producto.vendedor
            };
        }

        public static List<productoDto> ToDTOs(this List<producto> productos)
        {
            return productos.Select(x => x.ToDTO()).ToList();
        }
    }
}
