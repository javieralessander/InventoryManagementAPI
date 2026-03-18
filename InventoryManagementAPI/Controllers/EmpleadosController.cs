using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpleadosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("AllEmpleados")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            return await _context.Empleados.ToListAsync();
        }

        [HttpPost("CreateEmpleado")]
        public async Task<ActionResult<Empleado>> CreateEmpleado(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return Ok(empleado);
        }

        [HttpPut("ChangeEmpleado/{id}")]
        public async Task<IActionResult> UpdateEmpleado(int id, Empleado empleado)
        {
            if (id != empleado.Id)
                return BadRequest();

            _context.Entry(empleado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("DeleteEmpleado/{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
                return NotFound();

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("PatchEmpleado/{id}")]
        public async Task<IActionResult> PatchEmpleado(int id, Empleado empleado)
        {
            var empleadoDB = await _context.Empleados.FindAsync(id);

            if (empleadoDB == null)
                return NotFound();

            empleadoDB.Nombre = empleado.Nombre ?? empleadoDB.Nombre;
            empleadoDB.Rol = empleado.Rol ?? empleadoDB.Rol;

            await _context.SaveChangesAsync();

            return Ok(empleadoDB);
        }
    }
}