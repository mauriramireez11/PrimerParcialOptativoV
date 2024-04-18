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
    public class ClienteController : Controller
    {
        private ClienteService clienteService;
        private IConfiguration configuration;

        public ClienteController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.clienteService = new ClienteService(configuration.GetConnectionString("postgresDB"));
        }

        [HttpPost("InsertarCliente")]
        public ActionResult<string> insertar(ClienteModel modelo)
        {
            var resultado = this.clienteService.insertarCliente(new Repository.Data.ClienteModel
            {
                Id_banco = modelo.Id_banco,
                Nombre = modelo.Nombre,
                Apellido = modelo.Apellido,
                Documento = modelo.Documento,
                Direccion = modelo.Direccion,
                Mail = modelo.Mail,
                Celular = modelo.Celular,
                Estado = modelo.Estado

            });
            return Ok(resultado);
        }

        [HttpDelete("eliminarCliente/{Id}")]
        public ActionResult<string> eliminar(int Id)
        {
            var resultado = this.clienteService.eliminarCliente(Id);
            return Ok(resultado);
        }

        [HttpPut("modificarCliente/{Id}")]
        public ActionResult<string> modificar(ClienteModel modelo, int Id)
        {
            var resultado = this.clienteService.modificarCliente(new Repository.Data.ClienteModel
            {
                Id_banco = modelo.Id_banco,
                Nombre = modelo.Nombre,
                Apellido = modelo.Apellido,
                Documento = modelo.Documento,
                Direccion = modelo.Direccion,
                Mail = modelo.Mail,
                Celular = modelo.Celular,
                Estado = modelo.Estado
            }, Id);
            return Ok(resultado);
        }

        [HttpGet("ListarCliente")]
        public ActionResult<List<ClienteModelLista>> listar()
        {
            var resultado = clienteService.listarCliente();
            return Ok(resultado);
        }

        [HttpGet("ConsultarCliente/{Id}")]
        public ActionResult<ClienteModelLista> consultar(int Id)
        {
            var resultado = this.clienteService.consultarCliente(Id);
            return Ok(resultado);
        }
    }
}