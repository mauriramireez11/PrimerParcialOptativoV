using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repository
{
    public class FacturaRepository
    {
        private readonly AppDbContext _context;

        public FacturaRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public void InsertarFactura(FacturaModel factura)
        {
            _context.Facturas.Add(factura);
            _context.SaveChanges();
        }

        public void ModificarFactura(FacturaModel factura)
        {
            _context.Facturas.Update(factura);
            _context.SaveChanges();
        }

        public void EliminarFactura(int Id)
        {
            var factura = _context.Facturas.FirstOrDefault(f => f.Id == Id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                _context.SaveChanges();
            }
        }

        public FacturaModel ConsultarFactura(int Id)
        {
            return _context.Facturas.FirstOrDefault(f => f.Id == Id);
        }

        public IEnumerable<FacturaModel> ListarFactura()
        {
            return _context.Facturas.ToList();
        }

        public bool ExisteCliente(int Id)
        {
            try
            {
                return _context.Clientes.Any(c => c.Id == Id);
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
                return _context.Clientes.Any(c => _context.Facturas.Any(f => f.Id_cliente == idCliente));
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
                return _context.Facturas.Any(f => f.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}