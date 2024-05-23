using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Transactions;
using Repository.Models;
using Microsoft.Extensions.Logging;

namespace Repository.Repository
{
    public class ClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void insertarCliente(ClienteModel cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }


        public string modificarCliente(ClienteModel cliente, int Id)
        {
            try
            {
                var clienteExistente = _context.Clientes.FirstOrDefault(c => c.Id == Id);
                if (clienteExistente == null)
                {
                    return "No se encontró el cliente con el ID proporcionado.";
                }

                clienteExistente.Id_banco = cliente.Id_banco;
                clienteExistente.Nombre = cliente.Nombre;
                clienteExistente.Apellido = cliente.Apellido;
                clienteExistente.Documento = cliente.Documento;
                clienteExistente.Direccion = cliente.Direccion;
                clienteExistente.Mail = cliente.Mail;
                clienteExistente.Celular = cliente.Celular;
                clienteExistente.Estado = cliente.Estado;

                _context.SaveChanges();

                return "Se modificaron los datos correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void eliminarCliente(int Id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == Id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }
        public ClienteModel consultarCliente(int Id)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id == Id);
        }


        public IEnumerable<ClienteModel> listarCliente()
        {
            return _context.Clientes.ToList();
        }

    }
 }
