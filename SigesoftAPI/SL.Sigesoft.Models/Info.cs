using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Models
{
    public partial class Info
    {
        public Info()
        {
            Details = new HashSet<Detail>();
        }

        public string Ruc {get; set;}
        public string RazonSocial { get; set;}
        public string EstadoContribuyente { get; set;}
        public string CondicionDomicilio { get; set;}
        public string Ubigeo { get; set;}
        public string TipoVia { get; set;}
        public string NombreVia { get; set;}
        public string CodigoZona { get; set;}
        public string TipoZona { get; set;}
        public string Numero { get; set;}
        public string Interior { get; set;}
        public string Lote { get; set;}
        public string Departamento { get; set;}
        public string Manzana { get; set;}
        public string Kilometro { get; set;}
        public string Col { get; set;}

        public virtual ICollection<Detail> Details { get; set; }
    }
}
