using CartaSur.Domain.DTOs;
using CartaSur.Domain.Interfaces;
using CartaSur.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace CartaSur.Repository.Repositories;

public class VentaRepository : IVentaRepository
{
    private readonly AppDbContext _context;

    public VentaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FechaMaxVentasDto?> GetFechaConMasVentasAsync()
    {
        return await _context.Ventas
            .GroupBy(v => v.FechaVenta.Date)
            .Select(g => new FechaMaxVentasDto
            {
                Fecha = DateOnly.FromDateTime(g.Key),
                CantidadVentas = g.Count()
            })
            .OrderByDescending(x => x.CantidadVentas)
            .FirstOrDefaultAsync();
    }
}