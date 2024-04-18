using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Services.Logica;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Microsoft.AspNetCore.Authorization;

namespace PrimerParcial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FacturaController : Controller
    {
        private FacturaService facturaService;
        private IConfiguration configuration;

        public FacturaController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.facturaService = new FacturaService(configuration.GetConnectionString("postgresDB"));
        }

        [HttpPost("InsertarFactura")]
        public ActionResult<string> insertar(FacturaModel modelo)
        {
            var resultado = this.facturaService.InsertarFactura(new Repository.Data.FacturaModel
            {
                Id_cliente = modelo.Id_cliente,
                Nro_factura = modelo.Nro_factura,
                Fecha_hora = modelo.Fecha_hora,
                Total = modelo.Total,
                Total_iva5 = modelo.Total_iva5,
                Total_iva10 = modelo.Total_iva10,
                Total_iva = modelo.Total_iva,
                Total_letras = modelo.Total_letras,
                Sucursal = modelo.Sucursal
            });
            return Ok(resultado);
        }

        [HttpDelete("eliminarFactura/{Id}")]
        public ActionResult<string> eliminar(int Id)
        {
            var resultado = this.facturaService.eliminarFactura(Id);
            return Ok(resultado);
        }

        [HttpPut("modificarFactura/{Id}")]
        public ActionResult<string> modificar(FacturaModel modelo, int Id)
        {
            var resultado = this.facturaService.modificarFactura(new Repository.Data.FacturaModel
            {
                Id_cliente = modelo.Id_cliente,
                Nro_factura = modelo.Nro_factura,
                Fecha_hora = modelo.Fecha_hora,
                Total = modelo.Total,
                Total_iva5 = modelo.Total_iva5,
                Total_iva10 = modelo.Total_iva10,
                Total_iva = modelo.Total_iva,
                Total_letras = modelo.Total_letras,
                Sucursal = modelo.Sucursal
            }, Id);
            return Ok(resultado);
        }

        [HttpGet("ListarFactura")]
        public ActionResult<List<FacturaModelLista>> listar()
        {
            var resultado = facturaService.listarFactura();
            return Ok(resultado);
        }

        [HttpGet("ConsultarFactura/{Id}")]
        public ActionResult<FacturaModelLista> consultar(int Id)
        {
            var resultado = this.facturaService.consultarFactura(Id);
            return Ok(resultado);
        }
    }
}