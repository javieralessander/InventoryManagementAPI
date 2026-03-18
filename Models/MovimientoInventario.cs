public class MovimientoInventario
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public string TipoMovimiento { get; set; } // Entrada o Salida

    public int Cantidad { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;
}