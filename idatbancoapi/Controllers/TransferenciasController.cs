using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using idatbancoapi.Data;
using idatbancoapi.Models;

namespace idatbancoapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferenciasController : ControllerBase
    {
        private readonly IdatBankContext _context;

        public TransferenciasController(IdatBankContext context)
        {
            _context = context;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Transferencium>>> Listar()
        {
            return await _context.Transferencia
                .Include(t => t.CuentaOrigen)
                .Include(t => t.CuentaDestino)
                .ToListAsync();
        }



        [HttpGet("buscar/{id}")]
        public async Task<ActionResult<Transferencium>> Buscar(int id)
        {
            var transferencia = await _context.Transferencia
                .Include(t => t.CuentaOrigen)
                .Include(t => t.CuentaDestino)
                .FirstOrDefaultAsync(t => t.TransferenciaId == id);

            if (transferencia == null)
                return NotFound(new { mensaje = "Transferencia no encontrada" });

            return transferencia;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<Transferencium>> Crear([FromBody] Transferencium transferencia)
        {
            var cuentaOrigen = await _context.Cuenta.FindAsync(transferencia.CuentaOrigenId);
            var cuentaDestino = await _context.Cuenta.FindAsync(transferencia.CuentaDestinoId);
            if (cuentaOrigen == null || cuentaDestino == null)
                return BadRequest(new { mensaje = "Cuenta origen o destino no existe" });

            if (cuentaOrigen.Saldo < transferencia.Monto)
                return BadRequest(new { mensaje = "Saldo insuficiente en cuenta origen" });

            transferencia.Fecha = DateTime.Now;
            _context.Transferencia.Add(transferencia);

            cuentaOrigen.Saldo -= transferencia.Monto;
            cuentaDestino.Saldo += transferencia.Monto;

            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Transferencia realizada correctamente", data = transferencia });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var transferencia = await _context.Transferencia.FindAsync(id);
            if (transferencia == null)
                return NotFound(new { mensaje = "Transferencia no encontrada" });

            _context.Transferencia.Remove(transferencia);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Transferencia eliminada correctamente" });
        }
    }
}
