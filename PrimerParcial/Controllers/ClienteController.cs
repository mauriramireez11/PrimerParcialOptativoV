using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Repository.Repository;
using Services;

namespace PrimerParcial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository _clienteRepository;
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteRepository clienteRepository, ClienteService clienteService)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
        }

        [HttpPost("InsertarCliente")]
        public IActionResult PostCliente([FromBody] ClienteModel cliente)
        {
            if (!_clienteService.ValidarCliente(cliente))
                return BadRequest("Los datos del cliente no son validos, verifique nuevamente.");

            _clienteRepository.insertarCliente(cliente);
            return Ok("Cliente agregado correctamente.");
        }

        [HttpDelete("eliminarCliente/{id}")]
        public IActionResult EliminarCliente(int id)
        {
            var clienteExiste = _clienteRepository.consultarCliente(id);
            if (clienteExiste == null)
                return NotFound("Cliente no encontrado");

            _clienteRepository.eliminarCliente(id);
            return Ok("Cliente eliminado correctamente.");
        }

        [HttpPut("modificarCliente")]
        public IActionResult ActualizarCliente([FromBody] ClienteModel cliente)
        {
            var clienteExistente = _clienteRepository.consultarCliente(cliente.Id);
            if (clienteExistente == null)
                return NotFound("Cliente no encontrado");

            if (!_clienteService.ValidarCliente(cliente))
                return BadRequest("Los datos ingresados no son válidos");

            clienteExistente.Id_banco = cliente.Id_banco;
            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Apellido = cliente.Apellido;
            clienteExistente.Documento = cliente.Documento;
            clienteExistente.Direccion = cliente.Direccion;
            clienteExistente.Mail = cliente.Mail;
            clienteExistente.Celular = cliente.Celular;
            clienteExistente.Estado = cliente.Estado;

            try
            {
                _clienteRepository.modificarCliente(clienteExistente, cliente.Id);
                return Ok("Cliente modificado sin inconvenientes");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al modificar el cliente: {ex.Message}");
            }
        }

        [HttpGet("ListarCliente")]
        public IActionResult ObtenerClientes()
        {
            var clientes = _clienteRepository.listarCliente();
            return Ok(clientes);
        }

        [HttpGet("ConsultarCliente/{Id}")]
        public IActionResult ListarClientePorId(int Id)
        {
            var cliente = _clienteRepository.consultarCliente(Id);
            if (cliente == null)
                return NotFound("El cliente no existe.");

            return Ok(cliente);
        }
    }
}