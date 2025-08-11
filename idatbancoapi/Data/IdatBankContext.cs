using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using idatbancoapi.Models;

namespace idatbancoapi.Data;

public partial class IdatBankContext : DbContext
{
    public IdatBankContext()
    {
    }

    public IdatBankContext(DbContextOptions<IdatBankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Transferencium> Transferencia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:idatbankserver.database.windows.net,1433;Initial Catalog=IdatBank;Persist Security Info=False;User ID=admin12345;Password=@Temamaste9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Cliente__71ABD0878100E3BD");
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.CuentaId).HasName("PK__Cuenta__40072E8111A4DB55");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cuenta__ClienteI__619B8048");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.MovimientoId).HasName("PK__Movimien__BF923C2CF91619F0");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Movimientos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__Cuent__656C112C");
        });

        modelBuilder.Entity<Transferencium>(entity =>
        {
            entity.HasKey(e => e.TransferenciaId).HasName("PK__Transfer__E5B4F5D240528DB9");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CuentaDestino).WithMany(p => p.TransferenciumCuentaDestinos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transfere__Cuent__6A30C649");

            entity.HasOne(d => d.CuentaOrigen).WithMany(p => p.TransferenciumCuentaOrigens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transfere__Cuent__693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
