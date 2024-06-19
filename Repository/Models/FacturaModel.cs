using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class FacturaModel
    {
        public int Id { get; set; }
        public int Id_cliente { get; set; }
        [RegularExpression(@"^\d{3}-\d{3}-\d{6}$")]
        public string Nro_factura { get; set; }
        public string Fecha_hora { get; set; }
        public int Total { get; set; }
        public int Total_iva { get; set; }
        public int Total_iva10 { get; set; }
        public int Total_iva5 { get; set; }
        public string Total_letras { get; set; }
        public string Sucursal { get; set; }

    }
}

