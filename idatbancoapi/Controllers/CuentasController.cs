using Microsoft.AspNetCore.Mvc;
using idatbancoapi.Data;
using idatbancoapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace idatbancoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly IdatBankContext _context;

        public CuentasController(IdatBankContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Listar")]
        public IActionResult ListarCuentas()
        {
            var cuentas = _context.Cuenta
                .Include(c => c.Cliente)
                .ToList();
            return Ok(cuentas);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult BuscarCuenta(int id)
        {
            var cuenta = _context.Cuenta
                .Include(c => c.Cliente)
                .FirstOrDefault(c => c.CuentaId == id);

            if (cuenta == null)
                return NotFound(new { mensaje = "Cuenta no encontrada" });

            return Ok(cuenta);
        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult CrearCuenta([FromBody] Cuentum cuenta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteExiste = _context.Clientes.Any(c => c.ClienteId == cuenta.ClienteId);
            if (!clienteExiste)
                return BadRequest(new { mensaje = "El cliente no existe" });

            var numeroExiste = _context.Cuenta.Any(c => c.NumeroCuenta == cuenta.NumeroCuenta);
            if (numeroExiste)
                return BadRequest(new { mensaje = "El número de cuenta ya existe" });

            _context.Cuenta.Add(cuenta);
            _context.SaveChanges();

            return Ok(new { mensaje = "Cuenta creada correctamente", data = cuenta });
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public IActionResult EditarCuenta(int id, [FromBody] Cuentum cuenta)
        {
            if (cuenta.CuentaId != id)
                return BadRequest(new { mensaje = "El ID de la cuenta no coincide" });

            var cuentaExiste = _context.Cuenta.Any(c => c.CuentaId == id);
            if (!cuentaExiste)
                return NotFound(new { mensaje = "Cuenta no encontrada" });

            _context.Entry(cuenta).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(new { mensaje = "Cuenta actualizada correctamente" });
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public IActionResult EliminarCuenta(int id)
        {
            var cuenta = _context.Cuenta.FirstOrDefault(c => c.CuentaId == id);
            if (cuenta == null)
                return NotFound(new { mensaje = "Cuenta no encontrada" });

            _context.Cuenta.Remove(cuenta);
            _context.SaveChanges();

            return Ok(new { mensaje = "Cuenta eliminada correctamente" });
        }
    }
}
