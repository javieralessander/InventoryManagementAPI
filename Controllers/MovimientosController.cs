using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class MovimientosController : ControllerBase
{
    private readonly AppDbContext _context;

    public MovimientosController(AppDbContext context)
    {
        _context = context;
    }

    // Registrar movimiento
    [HttpPost("CreateMovimiento")]
    public async Task<IActionResult> CreateMovimiento(MovimientoInventario movimiento)
    {
        var producto = await _context.Productos.FindAsync(movimiento.ProductoId);

        if (producto == null)
            return NotFound("Producto no encontrado");

        if (movimiento.TipoMovimiento == "Entrada")
        {
            producto.Stock += movimiento.Cantidad;
        }
        else if (movimiento.TipoMovimiento == "Salida")
        {
            producto.Stock -= movimiento.Cantidad;
        }

        _context.MovimientosInventario.Add(movimiento);
        await _context.SaveChangesAsync();

        return Ok(movimiento);
    }

    // Ver movimientos
    [HttpGet("AllMovimientos")]
    public async Task<IActionResult> GetMovimientos()
    {
        return Ok(await _context.MovimientosInventario.ToListAsync());
    }
}