using System;
using System.Collections.Generic;

namespace Persistencia.Models;

public partial class cliente
{
    public int clienteid { get; set; }

    public int? personaid { get; set; }

    public string? departamento { get; set; }

    public string? provincia { get; set; }

    public string direccion { get; set; } = null!;

    public string referencia { get; set; } = null!;

    public string? correoalternativo { get; set; }

    public string nrocelular { get; set; } = null!;

    public DateTime fecharegistro { get; set; }

    public virtual persona? persona { get; set; }
}
