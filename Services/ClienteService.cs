using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repository;

namespace Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public bool ValidarCliente(ClienteModel cliente)
        {


            if (string.IsNullOrEmpty(cliente.Nombre) || cliente.Nombre.Length < 3)
            {

                return false;
            }

            if (string.IsNullOrEmpty(cliente.Apellido) || cliente.Apellido.Length < 3)
            {

                return false;
            }

            if (string.IsNullOrEmpty(cliente.Documento) || cliente.Documento.Length < 3)
            {

                return false;
            }

            if (!string.IsNullOrEmpty(cliente.Celular))
            {
                if (!EsNumero(cliente.Celular))
                {

                    return false;
                }

                if (cliente.Celular.Length != 10)
                {

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