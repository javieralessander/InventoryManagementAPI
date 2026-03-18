using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InventarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("AllInventario")]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetInventario()
        {
            return await _context.MovimientosInventario.ToListAsync();
        }

        [HttpPost("CreateMovimiento")]
        public async Task<ActionResult<MovimientoInventario>> CreateMovimiento(MovimientoInventario movimiento)
        {
            movimiento.Fecha = DateTime.Now;

            _context.MovimientosInventario.Add(movimiento);
            await _context.SaveChangesAsync();

            return Ok(movimiento);
        }

        [HttpDelete("DeleteMovimiento/{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _context.MovimientosInventario.FindAsync(id);

            if (movimiento == null)
                return NotFound();

            _context.MovimientosInventario.Remove(movimiento);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("PatchMovimiento/{id}")]
        public async Task<IActionResult> PatchMovimiento(int id, MovimientoInventario movimiento)
        {
            var movimientoDB = await _context.MovimientosInventario.FindAsync(id);

            if (movimientoDB == null)
                return NotFound();

            movimientoDB.Cantidad = movimiento.Cantidad;
            movimientoDB.TipoMovimiento = movimiento.TipoMovimiento;

            await _context.SaveChangesAsync();

            return Ok(movimientoDB);
        }
    }
}