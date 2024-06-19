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
            var validador = new Services.ClienteService.ValidarClienteFluent(_clienteRepository);
            var resultado = validador.Validate(cliente);

            if (!resultado.IsValid)
                return BadRequest(string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage)));

            _clienteRepository.insertarCliente(cliente);
            return Ok("Cliente agregado correctamente.");
        }

        [HttpPut("modificarCliente")]
        public IActionResult ActualizarCliente([FromBody] ClienteModel cliente)
        {
            var clienteExistente = _clienteRepository.consultarCliente(cliente.Id);
            if (clienteExistente == null)
                return NotFound("Cliente no encontrado");

            var validador = new Services.ClienteService.ValidarClienteFluent(_clienteRepository);
            var resultado = validador.Validate(cliente);

            if (!resultado.IsValid)
                return BadRequest(string.Join(", ", resultado.Errors.Select(e => e.ErrorMessage)));

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
        [HttpDelete("eliminarCliente/{id}")]
        public IActionResult EliminarCliente(int id)
        {
            var clienteExiste = _clienteRepository.consultarCliente(id);
            if (clienteExiste == null)
                return NotFound("Cliente no encontrado");

            _clienteRepository.eliminarCliente(id);
            return Ok("Cliente eliminado correctamente.");
        }

        [HttpGet("ListarCliente")]
        public IActionResult ListarClientes()
        {
            var clientes = _clienteRepository.ListarClientesActivos();
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