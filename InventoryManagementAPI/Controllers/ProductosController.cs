using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/AllProductos
        [HttpGet("AllProductos")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // POST /api/CreateProducto
        [HttpPost("CreateProducto")]
        public async Task<ActionResult<Producto>> CreateProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return Ok(producto);
        }

        // PUT /api/ChangeProducto/{id}
        [HttpPut("ChangeProducto/{id}")]
        public async Task<IActionResult> UpdateProducto(int id, Producto producto)
        {
            if (id != producto.Id)
                return BadRequest();

            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE /api/DeleteProducto/{id}
        [HttpDelete("DeleteProducto/{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PATCH /api/PatchProducto/{id}
        [HttpPatch("PatchProducto/{id}")]
        public async Task<IActionResult> PatchProducto(int id, [FromBody] Producto producto)
        {
            var productoDB = await _context.Productos.FindAsync(id);

            if (productoDB == null)
                return NotFound();

            productoDB.Nombre = producto.Nombre ?? productoDB.Nombre;
            productoDB.Categoria = producto.Categoria ?? productoDB.Categoria;

            await _context.SaveChangesAsync();

            return Ok(productoDB);
        }

        [HttpGet("StockBajo")]
        public async Task<IActionResult> StockBajo()
        {
            var productos = await _context.Productos
                .Where(p => p.Stock <= p.StockMinimo)
                .ToListAsync();

            return Ok(productos);
        }
    }
}