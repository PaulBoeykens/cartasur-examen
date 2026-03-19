using CartaSur.Domain.DTOs;
using CartaSur.Domain.Interfaces;

namespace CartaSur.Service.Services;

public class VentaService : IVentaService
{
    private readonly IVentaRepository _ventaRepository;

    public VentaService(IVentaRepository ventaRepository)
    {
        _ventaRepository = ventaRepository;
    }

    public async Task<FechaMaxVentasDto?> ObtenerFechaConMasVentasAsync()
    {
        return await _ventaRepository.GetFechaConMasVentasAsync();
    }
}