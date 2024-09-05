using System;
using System.Collections.Generic;

namespace Persistencia.Models;

public partial class persona
{
    public int personaid { get; set; }

    public string nombres { get; set; } = null!;

    public string apellidos { get; set; } = null!;

    public int tipodocidentidad { get; set; }

    public string? nrodocidentidad { get; set; }

    public DateOnly? fechanacimiento { get; set; }

    public DateTime fecharegistro { get; set; }

    public int rolid { get; set; }

    public virtual ICollection<cliente> clientes { get; set; } = new List<cliente>();

    public virtual ICollection<usuario> usuarios { get; set; } = new List<usuario>();
}
