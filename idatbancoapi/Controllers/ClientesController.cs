using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using idatbancoapi.Data;
using idatbancoapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace idatbancoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IdatBankContext _context;

        public ClientesController(IdatBankContext context)
        {
            _context = context;
        }

        [HttpGet("/")]
        public IActionResult Home()
        {
            var clientes = _context.Clientes.ToList();
            return Ok(clientes);
        }

        [HttpGet]
        [Route("Listar")]
        public IActionResult ListarClientes()
        {
            var clientes = _context.Clientes.ToList();
            return Ok(clientes);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult BuscarCliente(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.ClienteId == id);

            if (cliente == null)
            {
                return NotFound(new { mensaje = "Cliente no encontrado" });
            }

            return Ok(cliente);
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return Ok(new { mensaje = "Cliente creado correctamente", data = cliente });
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public IActionResult EditarCliente(int id, [FromBody] Cliente cliente)
        {
            if (cliente.ClienteId != id)
            {
                return BadRequest(new { mensaje = "El ID del cliente no coincide" });
            }

            var clienteExiste = _context.Clientes.Any(c => c.ClienteId == id);
            if (!clienteExiste)
            {
                return NotFound(new { mensaje = "Cliente no encontrado" });
            }

            _context.Entry(cliente).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { mensaje = "Cliente actualizado correctamente" });
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarCliente(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.ClienteId == id);

            if (cliente == null)
            {
                return NotFound(new { mensaje = "Cliente no encontrado" });
            }

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return Ok(new { mensaje = "Cliente eliminado correctamente" });
        }
    }
}
