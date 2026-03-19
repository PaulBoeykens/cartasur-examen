using CartaSur.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartaSur.Repository.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Venta> Ventas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Venta>(entity =>
        {
            entity.ToTable("VENTAS");
            entity.HasKey(e => e.IdVenta);
            entity.Property(e => e.IdVenta).HasColumnName("ID_VENTA");
            entity.Property(e => e.FechaVenta).HasColumnName("Fecha_venta");
            entity.Property(e => e.DniCliente).HasColumnName("Dni_cliente");
            entity.Property(e => e.NombreEmpleado).HasColumnName("Nombre_empleado");
            entity.Property(e => e.NombreCliente).HasColumnName("Nombre_cliente");
            entity.Property(e => e.ImporteTotal).HasColumnName("Importe_total");
            entity.Property(e => e.DireccionEnvioCliente).HasColumnName("Direccion_envio_cliente");
            entity.Property(e => e.DireccionSucursalVenta).HasColumnName("Direccion_sucursal_venta");
            entity.Property(e => e.NombreSucursalVenta).HasColumnName("Nombre_sucursal_venta");
            entity.Property(e => e.Producto).HasColumnName("Producto");
            entity.Property(e => e.Cantidad).HasColumnName("Cantidad");
        });
    }
}
