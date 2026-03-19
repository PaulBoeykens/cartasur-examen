using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartaSur.Domain.Entities;

public class Venta
{
    public int IdVenta { get; set; }
    public DateTime FechaVenta { get; set; }
    public string? DniCliente { get; set; }
    public string? NombreEmpleado { get; set; }
    public string? NombreCliente { get; set; }
    public decimal ImporteTotal { get; set; }
    public string? DireccionEnvioCliente { get; set; }
    public string? DireccionSucursalVenta { get; set; }
    public string? NombreSucursalVenta { get; set; }
    public string? Producto { get; set; }
    public int Cantidad { get; set; }
}
