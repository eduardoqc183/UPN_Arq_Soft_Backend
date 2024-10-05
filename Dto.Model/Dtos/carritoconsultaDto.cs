using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Model.Dtos
{
    public class carritoconsultaDto
    {
        public int CarritoId { get; set; }
        public int UsuarioId { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Completado { get; set; }
        public decimal Precio { get; set; }
        public string FotoUrl { get; set; }
    }
}
