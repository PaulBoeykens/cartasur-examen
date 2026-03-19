using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartaSur.Domain.DTOs;

public class FechaMaxVentasDto
{
    public DateOnly Fecha { get; set; }
    public int CantidadVentas { get; set; }
}
