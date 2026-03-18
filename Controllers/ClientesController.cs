using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("AllClientes")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpPost("CreateCliente")]
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        [HttpPut("ChangeCliente/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
                return BadRequest();

            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("DeleteCliente/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound();

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("PatchCliente/{id}")]
        public async Task<IActionResult> PatchCliente(int id, Cliente cliente)
        {
            var clienteDB = await _context.Clientes.FindAsync(id);

            if (clienteDB == null)
                return NotFound();

            clienteDB.Nombre = cliente.Nombre ?? clienteDB.Nombre;
            clienteDB.Telefono = cliente.Telefono ?? clienteDB.Telefono;
            clienteDB.Correo = cliente.Correo ?? clienteDB.Correo;

            await _context.SaveChangesAsync();

            return Ok(clienteDB);
        }
    }
}