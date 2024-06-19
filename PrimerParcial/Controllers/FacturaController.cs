using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Microsoft.AspNetCore.Authorization;
using Services;
using Repository.Models;
using Repository.Repository;
using static Services.FacturaService;

namespace PrimerParcial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaRepository _facturaRepository;
        private readonly FacturaService _facturaService;

        public FacturaController(FacturaRepository facturaRepository, FacturaService facturaService)
        {
            _facturaRepository = facturaRepository;
            _facturaService = facturaService;
        }

        [HttpPost("InsertarFactura")]
        public IActionResult insercionFactura([FromBody] FacturaModel factura)
        {
            var validador = new ValidarFacturaFluent(_facturaRepository);
            var resultado = validador.Validate(factura);

            if (!resultado.IsValid)
                return BadRequest(string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage)));

            _facturaRepository.InsertarFactura(factura);
            return Ok("La factura se inserto correctamente.");
        }

        [HttpPut("modificarFactura")]
        public IActionResult ActualizarFactura([FromBody] FacturaModel factura)
        {
            var facturaExistente = _facturaRepository.ConsultarFactura(factura.Id);
            if (facturaExistente == null)
                return NotFound("Factura no encontrada");

            var validador = new ValidarFacturaFluent(_facturaRepository);
            var resultado = validador.Validate(factura);

            if (!resultado.IsValid)
                return BadRequest(string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage)));

            facturaExistente.Id_cliente = factura.Id_cliente;
            facturaExistente.Nro_factura = factura.Nro_factura;
            facturaExistente.Fecha_hora = factura.Fecha_hora;
            facturaExistente.Total = factura.Total;
            facturaExistente.Total_iva5 = factura.Total_iva5;
            facturaExistente.Total_iva10 = factura.Total_iva10;
            facturaExistente.Total_iva = factura.Total_iva;
            facturaExistente.Total_letras = factura.Total_letras;
            facturaExistente.Sucursal = factura.Sucursal;

            try
            {
                _facturaRepository.ModificarFactura(facturaExistente);
                return Ok("Factura modificada sin inconvenientes");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al modificar la factura: {ex.Message}");
            }
        }

        [HttpDelete("eliminarFactura/{Id}")]
        public IActionResult DeleteFactura(int Id)
        {
            var factura = _facturaRepository.ConsultarFactura(Id);
            if (factura == null)
            {
                return NotFound();
            }

            _facturaRepository.EliminarFactura(Id);
            return Ok("Factura eliminada correctamente.");
        }

        [HttpGet("ListarFactura")]
        public IActionResult ObtenerFacturas()
        {
            var facturas = _facturaRepository.ListarFactura();
            return Ok(facturas);
        }

        [HttpGet("ConsultarFactura/{Id}")]
        public IActionResult ObtenerFacturaPorId(int Id)
        {
            var factura = _facturaRepository.ConsultarFactura(Id);
            if (factura == null)
                return NotFound("La factura no existe.");
            
            return Ok(factura);
        }
    }
}