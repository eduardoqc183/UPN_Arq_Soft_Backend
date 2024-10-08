﻿using System;
using System.Collections.Generic;

namespace Persistencia.Models;

public partial class pago
{
    public int pagoid { get; set; }

    public int monedaid { get; set; }

    public int formapagoid { get; set; }

    public decimal valortotal { get; set; }

    public int porcentajeigv { get; set; }

    public decimal igv { get; set; }

    public decimal total { get; set; }

    public DateTime fecharegistro { get; set; }

    public int? usuarioid { get; set; }

    public virtual ICollection<carritocompra> carritocompras { get; set; } = new List<carritocompra>();

    public virtual usuario? usuario { get; set; }
}
