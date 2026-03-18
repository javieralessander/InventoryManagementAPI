using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Categoria { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public int StockMinimo { get; set; }
    }
}