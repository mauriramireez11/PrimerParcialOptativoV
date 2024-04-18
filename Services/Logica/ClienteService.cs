using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Data;

namespace Services.Logica
{
    public class ClienteService
    {
        private ClienteRepository clienteRepository;

    public ClienteService(string connectionString)
    {
        this.clienteRepository = new ClienteRepository(connectionString);
    }

        public string insertarCliente(ClienteModel cliente)
        {
            string errorMessage;
            return ValidarCliente(cliente, out errorMessage) ? clienteRepository.insertarCliente(cliente) : throw new Exception(errorMessage);
        }

        public string eliminarCliente(int Id)
        {
            string errorMessage;
            if (clienteRepository.ExisteCliente(Id))
            {
                return clienteRepository.eliminarCliente(Id);
            }
            else
            {
                return("El cliente con el ID especificado no existe.");
            }
        }
   
      

        public string modificarCliente(ClienteModel cliente, int Id)
        {
            string errorMessage;
            if (clienteRepository.ExisteCliente(Id))
            {
                if (ValidarCliente(cliente, out errorMessage))
                {
                    return clienteRepository.modificarCliente(cliente, Id);
                }
                else
                {
                    throw new Exception(errorMessage);
                }
            }
            else
            {
                return "No se encontraron los datos de este cliente";
            }
        }

        public ClienteModelLista consultarCliente(int Id_cliente)
        {
            string errorMessage;
            if (clienteRepository.ExisteCliente(Id_cliente))
            {
                return clienteRepository.consultarCliente(Id_cliente);
            }
            else
            {
                throw new Exception("El cliente con el ID especificado no existe.");
            }
        }

        public IEnumerable<ClienteModelLista> listarCliente()
        {
            return clienteRepository.listarCliente();
        }

        private bool ValidarCliente(ClienteModel cliente, out string errorMessage)
        {
            errorMessage = "";

            if (string.IsNullOrEmpty(cliente.Nombre) || cliente.Nombre.Length < 3)
            {
                errorMessage = "El nombre es inválido";
                return false;
            }

            if (string.IsNullOrEmpty(cliente.Apellido) || cliente.Apellido.Length < 3)
            {
                errorMessage = "El apellido es inválido";
                return false;
            }

            if (string.IsNullOrEmpty(cliente.Documento) || cliente.Documento.Length < 3)
            {
                errorMessage = "El documento es inválido";
                return false;
            }

            if (!string.IsNullOrEmpty(cliente.Celular))
            {
                if (!EsNumero(cliente.Celular))
                {
                    errorMessage = "El número de celular es inválido";
                    return false;
                }

                if (cliente.Celular.Length != 10)
                {
                    errorMessage = "El número de celular debe tener 10 dígitos";
                    return false;
                }
            }

            // Si todas las validaciones pasan, el cliente es válido
            return true;
        }

        private bool EsNumero(string valor)
        {
            return long.TryParse(valor, out _);
        }
        
    }
}