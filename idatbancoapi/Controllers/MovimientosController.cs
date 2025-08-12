using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using idatbancoapi.Data;
using idatbancoapi.Models;

namespace idatbancoapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IdatBankContext _context;

        public MovimientosController(IdatBankContext context)
        {
            _context = context;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Movimiento>>> Listar()
        {
            return await _context.Movimientos
                .Include(m => m.Cuenta)
                .ToListAsync();
        }

        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<Movimiento>> Buscar(int id)
        {
            var movimiento = await _context.Movimientos
                .Include(m => m.Cuenta)
                .FirstOrDefaultAsync(m => m.MovimientoId == id);

            if (movimiento == null)
                return NotFound(new { mensaje = "Movimiento no encontrado" });

            return movimiento;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] Movimiento movimiento)
        {
            var cuentaExiste = await _context.Cuenta.AnyAsync(c => c.CuentaId == movimiento.CuentaId);
            if (!cuentaExiste)
                return BadRequest(new { mensaje = "La cuenta no existe" });

            if (movimiento.Fecha == DateTime.MinValue)
                movimiento.Fecha = DateTime.Now;

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Movimiento creado correctamente", data = movimiento });
        }

        [HttpPut("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] Movimiento movimiento)
        {
            if (id != movimiento.MovimientoId)
                return BadRequest(new { mensaje = "El ID no coincide" });

            _context.Entry(movimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Movimientos.AnyAsync(m => m.MovimientoId == id))
                    return NotFound(new { mensaje = "Movimiento no encontrado" });
                else
                    throw;
            }

            return Ok(new { mensaje = "Movimiento actualizado correctamente" });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
                return NotFound(new { mensaje = "Movimiento no encontrado" });

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Movimiento eliminado correctamente" });
        }
    }
}
