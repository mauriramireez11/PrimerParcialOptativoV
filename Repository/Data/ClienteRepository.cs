using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Transactions;

namespace Repository.Data
{
    public class ClienteRepository
    {
        private string _connectionString;
        private Npgsql.NpgsqlConnection connection;
        public ClienteRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this.connection = new Npgsql.NpgsqlConnection(this._connectionString);
        }

        public string insertarCliente(ClienteModel cliente)
        {
            try
            {
                System.String query = "INSERT INTO cliente(id_banco, nombre, apellido, documento, direccion, mail, celular, estado) " +
                "VALUES (@id_banco, @nombre, @apellido, @documento, @direccion, @mail, @celular, @estado)";
                connection.Open();
                connection.Execute(query, cliente);
                connection.Close();

                return "Se inserto correctamente...";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string modificarCliente(ClienteModel cliente, int Id)
        {
            try
            {
                connection.Execute($"UPDATE cliente SET " +
                    "id_banco = @id_banco, " +
                    "nombre = @nombre, " +
                    "apellido = @apellido, " +
                    "documento = @documento, " +
                    "direccion = @direccion," +
                    "mail = @mail, " +
                    "celular = @celular, " +
                    "estado = @estado " +
                    $"WHERE id = {Id}", cliente);
                return "Se modificaron los datos correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string eliminarCliente(int Id)
        {
            try
            {
                // Eliminar las facturas asociadas al cliente
                connection.Execute("DELETE FROM factura WHERE id_cliente = @Id", new { Id });

                // Eliminar el cliente
                connection.Execute("DELETE FROM cliente WHERE id = @Id", new { Id });

                return "Se eliminó el registro en la tabla de cliente y también los registros de las facturas relacionadas al cliente.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ClienteModelLista consultarCliente(int Id)
        {
            try
            {
                return connection.QueryFirst<ClienteModelLista>($"SELECT * FROM cliente WHERE id= {Id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<ClienteModelLista> listarCliente()
        {
            try
            {
                return connection.Query<ClienteModelLista>("SELECT * FROM cliente");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ExisteCliente(int Id)
        {
            try
            {
                var exists = connection.QueryFirstOrDefault<bool>("SELECT EXISTS(SELECT 1 FROM cliente WHERE id = @id)", new { id = Id });
                return exists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
