using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Model.Dtos
{
    public class usuarioDto
    {
        public int usuarioid { get; set; }

        public int personaid { get; set; }

        public string email { get; set; } = null!;
        public string NombreCompleto { get; set; }
    }
}
