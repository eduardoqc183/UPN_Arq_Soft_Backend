using System;
using System.Collections.Generic;

namespace Persistencia.Models;

public partial class usuario
{
    public int usuarioid { get; set; }

    public int personaid { get; set; }

    public string email { get; set; } = null!;

    public string password { get; set; } = null!;

    public DateTime fecharegistro { get; set; }

    public virtual ICollection<carritocompra> carritocompras { get; set; } = new List<carritocompra>();

    public virtual persona persona { get; set; } = null!;
}
