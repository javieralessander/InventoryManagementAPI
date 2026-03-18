using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public string Correo { get; set; }
    }
}