using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CartaSur.Domain.DTOs;

namespace CartaSur.Domain.Interfaces;

public interface IVentaService
{
    Task<FechaMaxVentasDto?> ObtenerFechaConMasVentasAsync();
}