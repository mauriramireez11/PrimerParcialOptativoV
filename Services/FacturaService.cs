using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class FacturaService
    {

        public bool ValidarFactura(FacturaModel factura)
        {

            // Validar el formato de la factura
            if (!Regex.IsMatch(factura.Nro_factura, @"^\d{3}-\d{3}-\d{6}$"))
            {
                return false;
            }

            // Validar el campo Total
            if (string.IsNullOrEmpty(factura.Total) || !Regex.IsMatch(factura.Total, @"^\d+(\.\d{2})?$"))
            {
                return false;
            }

            // Validar el campo Total con IVA al 5%
            if (string.IsNullOrEmpty(factura.Total_iva5) || !Regex.IsMatch(factura.Total_iva5, @"^\d+(\.\d{2})?$"))
            {
                return false;
            }

            // Validar el campo Total con IVA al 10%
            if (string.IsNullOrEmpty(factura.Total_iva10) || !Regex.IsMatch(factura.Total_iva10, @"^\d+(\.\d{2})?$"))
            {
                return false;
            }

            // Validar el campo Total con IVA
            if (string.IsNullOrEmpty(factura.Total_iva) || !Regex.IsMatch(factura.Total_iva, @"^\d+(\.\d{2})?$"))
            {
                return false;
            }

            // Validar el campo Total en letras
            if (string.IsNullOrEmpty(factura.Total_letras) || factura.Total_letras.Length < 6)
            {
                return false;
            }

            return true;
        }
    }
}
