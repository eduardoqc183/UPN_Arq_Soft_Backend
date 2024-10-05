using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Model.Dtos
{
    public class pagoDto
    {
        public int pagoid { get; set; }

        public int monedaid { get; set; }

        public int formapagoid { get; set; }

        public decimal valortotal { get; set; }

        public int porcentajeigv { get; set; }

        public decimal igv { get; set; }

        public decimal total { get; set; }
        public string FormaPago
        {
            get
            {
                switch (formapagoid)
                {
                    case 1: return "EFECTIVO";
                    case 2: return "TARJETA";
                    case 3: return "YAPE";
                    default:
                        return "--";
                }
            }
        }

        public string Moneda
        {
            get
            {
                switch (monedaid)
                {
                    case 1: return "SOLES";
                    case 2: return "DÓLARES";
                    case 3: return "EUROS";
                    default:
                        return "--";
                }
            }
        }
        public DateTime fecharegistro { get; set; }
    }
}
