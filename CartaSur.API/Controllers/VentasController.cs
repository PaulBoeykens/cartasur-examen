using CartaSur.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CartaSur.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentasController : ControllerBase
{
    private readonly IVentaService _ventaService;

    public VentasController(IVentaService ventaService)
    {
        _ventaService = ventaService;
    }

    [HttpGet("fecha-max-ventas")]
    public async Task<IActionResult> GetFechaMaxVentas()
    {
        var resultado = await _ventaService.ObtenerFechaConMasVentasAsync();

        if (resultado is null)
            return NotFound(new { mensaje = "No se encontraron ventas." });

        return Ok(resultado);
    }
}