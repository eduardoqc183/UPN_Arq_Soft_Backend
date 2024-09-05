using Dapper.Contrib.Extensions;
using System;
using System.Runtime.CompilerServices;

namespace Scrapper
{
    public class ProductoFalabellaDto
    {
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public decimal Precio { get; set; }
        public string Vendedor { get; set; }
        public string UrlFoto { get; set; }
    }

    [Table("producto")]
    public class producto
    {
        public string codigoproducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int monedaid { get; set; } = 1;
        public decimal preciosinigv { get; set; }
        public DateTime fecharegistro { get; set; } = DateTime.Now;
        public string imagenurl { get; set; }
        public string marca { get; set; }
        public string vendedor { get; set; }
    }

    public static class Ext
    {
        public static producto ToEntity(this ProductoFalabellaDto p)
        {
            return new producto
            {
                nombre = p.Nombre,
                marca = p.Marca,
                preciosinigv = p.Precio,
                vendedor = p.Vendedor,
                imagenurl = p.UrlFoto                
            };
        }
    }
}
