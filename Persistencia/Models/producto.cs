using System;
using System.Collections.Generic;

namespace Persistencia.Models;

public partial class producto
{
    public int productoid { get; set; }

    public string codigoproducto { get; set; } = null!;

    public string nombre { get; set; } = null!;

    public string? descripcion { get; set; }

    public int monedaid { get; set; }

    public decimal preciosinigv { get; set; }

    public DateTime fecharegistro { get; set; }

    public string? imagenurl { get; set; }

    public string? marca { get; set; }

    public string? vendedor { get; set; }

    public virtual ICollection<carritocompra> carritocompras { get; set; } = new List<carritocompra>();
}
