using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public string Rol { get; set; }
    }
}