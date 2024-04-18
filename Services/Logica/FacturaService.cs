using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services.Logica
{
    public class FacturaService : FacturaModel
    {
        private FacturaRepository facturaRepository;
        public FacturaService(string connectionString)
        {
            this.facturaRepository = new FacturaRepository(connectionString);

        }

        public string InsertarFactura(FacturaModel factura)
        {
            if (factura == null)
            {
                throw new ArgumentNullException(nameof(factura), "La factura no puede ser null");
            }

            string errorMessage;
            if (ValidarFactura(factura, out errorMessage))
            {
                bool clienteExiste = facturaRepository.ExisteCliente(factura.Id_cliente);
                if (clienteExiste)
                {
                    // Realizar la inserción de la factura
                    return facturaRepository.insertarFactura(factura);
                }
                else
                {
                    // El cliente no existe, devolver un mensaje de error
                    return "El ID de cliente especificado no es válido";
                }
            }
            else
            {
                // La factura no es válida, lanzar excepción con el mensaje de error
                throw new Exception(errorMessage);
            }
        }

        public string eliminarFactura(int Id)
        {
            try
            {
                bool existeFactura = facturaRepository.ExisteFactura(Id);

                 if (existeFactura)
                {
                    return facturaRepository.eliminarFactura(Id);
                }
                else
                {
                    throw new Exception("La factura con el ID especificado no existe.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string modificarFactura(FacturaModel factura, int Id)
        {
            string errorMessage;
            if (facturaRepository.consultarFactura(Id) != null)
            {
                if (ValidarFactura(factura, out errorMessage))
                {
                    bool clienteExiste = facturaRepository.ExisteClienteFactura(factura.Id_cliente);
                    if (clienteExiste)
                    {
                        // Realizar la modificación de la factura
                        return facturaRepository.modificarFactura(factura, Id);
                    }
                    else
                    {
                        // El cliente no existe, devolver un mensaje de error
                        return "El ID de cliente especificado no es válido";
                    }
                }
                else
                {
                    // La factura no es válida, lanzar excepción con el mensaje de error
                    throw new Exception(errorMessage);
                }
            }
            else
            {
                return "No se encontraron los datos de esta factura";
            }
        }

        public FacturaModelLista consultarFactura(int Id)
        {
            // Verificar si el ID existe en la tabla factura
            bool existeFactura = facturaRepository.ExisteFactura(Id);

            if (existeFactura)
            {
                // Si existe, consultar y devolver la factura
                return facturaRepository.consultarFactura(Id);
            }
            else
            {
                // Si no existe, lanzar una excepción o devolver un resultado indicando que no se encontró la factura
                throw new Exception("La factura con el ID especificado no existe.");
            }
        }

        public IEnumerable<FacturaModelLista> listarFactura()
        {
            return facturaRepository.listarFactura();
        }

        private bool ValidarFactura(FacturaModel factura, out string errorMessage)
        {
            errorMessage = "";

            // Validar el formato de la factura
            if (!Regex.IsMatch(factura.Nro_factura, @"^\d{3}-\d{3}-\d{6}$"))
            {
                errorMessage = "El formato de la factura es inválido";
                return false;
            }

            // Validar el campo Total
            if (string.IsNullOrEmpty(factura.Total) || !Regex.IsMatch(factura.Total, @"^\d+(\.\d{2})?$"))
            {
                errorMessage = "El formato del total es inválido";
                return false;
            }

            // Validar el campo Total con IVA al 5%
            if (string.IsNullOrEmpty(factura.Total_iva5) || !Regex.IsMatch(factura.Total_iva5, @"^\d+(\.\d{2})?$"))
            {
                errorMessage = "El formato del total con IVA al 5% es inválido";
                return false;
            }

            // Validar el campo Total con IVA al 10%
            if (string.IsNullOrEmpty(factura.Total_iva10) || !Regex.IsMatch(factura.Total_iva10, @"^\d+(\.\d{2})?$"))
            {
                errorMessage = "El formato del total con IVA al 10% es inválido";
                return false;
            }

            // Validar el campo Total con IVA
            if (string.IsNullOrEmpty(factura.Total_iva) || !Regex.IsMatch(factura.Total_iva, @"^\d+(\.\d{2})?$"))
            {
                errorMessage = "El formato del total con IVA es inválido";
                return false;
            }

            // Validar el campo Total en letras
            if (string.IsNullOrEmpty(factura.Total_letras) || factura.Total_letras.Length < 6)
            {
                errorMessage = "El campo Total en letras es inválido";
                return false;
            }

            return true;
        }
    }
}
