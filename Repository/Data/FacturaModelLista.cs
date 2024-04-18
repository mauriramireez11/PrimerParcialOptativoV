using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class FacturaModelLista
    {
        public int Id { get; set; }
        public int Id_cliente { get; set; }
        public string Nro_factura { get; set; }
        public string Fecha_Hora { get; set; }
        public string Total { get; set; }
        public string Total_iva5 { get; set; }
        public string Total_iva10 { get; set; }
        public string Total_iva { get; set; }
        public string Total_letras { get; set; }
        public string Sucursal { get; set; }
    }
}
