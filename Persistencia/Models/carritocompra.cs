using System;
using System.Collections.Generic;

namespace Persistencia.Models;

public partial class carritocompra
{
    public int carritocompraid { get; set; }

    public int usuarioid { get; set; }

    public int productoid { get; set; }

    public int cantidad { get; set; }

    public DateTime fecharegistro { get; set; }

    public bool completado { get; set; }

    public int? pagoid { get; set; }

    public virtual pago? pago { get; set; }

    public virtual producto producto { get; set; } = null!;

    public virtual usuario usuario { get; set; } = null!;
}
