using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class FacturaRepository
    {
        private string _connectionString;
        private Npgsql.NpgsqlConnection connection;
        public FacturaRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this.connection = new Npgsql.NpgsqlConnection(this._connectionString);
        }

        public string insertarFactura(FacturaModel factura)
        {
            try
            {
                string query = "INSERT INTO factura(id_cliente, nro_factura, fecha_hora, total, total_iva5, total_iva10, total_iva, total_letras, sucursal) " +
               "VALUES (@Id_cliente, @Nro_factura, @Fecha_Hora, @Total, @Total_iva5, @Total_iva10, @Total_iva, @Total_letras, @Sucursal)"; ;
                connection.Open();
                connection.Execute(query, factura);
                connection.Close();

                return "Se inserto correctamente...";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string modificarFactura(FacturaModel factura, int Id)
        {
            try
            {
                connection.Execute($"UPDATE factura SET " +
                   "id_cliente = @id_cliente, " +
                   "nro_factura = @nro_factura, " +
                   "fecha_hora = @fecha_hora, " +
                   "total = @total, " +
                   "total_iva5 = @total_iva5, " +
                   "total_iva10 = @total_iva10, " +
                   "total_iva = @total_iva, " +
                   "total_letras = @total_letras, " +
                   "sucursal = @sucursal " +
                   $"WHERE id = {Id}", factura);
                return "Se modificaron los datos correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string eliminarFactura(int Id)
        {
            try
            {
                connection.Execute($" DELETE FROM factura WHERE id= {Id}");
                return "Se eliminó correctamente el registro...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public FacturaModelLista consultarFactura(int Id)
        {
            try
            {
                var factura = connection.QueryFirstOrDefault<FacturaModelLista>($"SELECT * FROM factura WHERE id = {Id}");

                if (factura != null)
                {
                    return factura;
                }
                else
                {
                    // Manejar el caso en el que no se encontró la factura
                    throw new Exception("La factura con el ID especificado no existe.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<FacturaModelLista> listarFactura()
        {
            try
            {
                return connection.Query<FacturaModelLista>("SELECT * FROM factura");
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
                var cliente = connection.QueryFirstOrDefault<ClienteModelLista>("SELECT * FROM cliente WHERE id = @id", new { Id = Id });
                return cliente != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ExisteClienteFactura(int idCliente)
        {
            try
            {
                var exists = connection.QueryFirstOrDefault<bool>(@"
            SELECT EXISTS(
                SELECT 1
                FROM cliente
                WHERE id = (
                    SELECT id_cliente
                    FROM factura
                    WHERE id_cliente = @idCliente
                )
            )", new { idCliente });

                return exists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteFactura(int Id)
        {
            try
            {
                var exists = connection.QueryFirstOrDefault<bool>("SELECT EXISTS(SELECT 1 FROM factura WHERE id = @id)", new { id = Id });
                return exists;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
  } 

