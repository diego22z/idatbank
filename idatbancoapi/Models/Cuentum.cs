using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace idatbancoapi.Models;

[Index("NumeroCuenta", Name = "UQ__Cuenta__E039507BFE3BDF97", IsUnique = true)]
public partial class Cuentum
{
    [Key]
    public int CuentaId { get; set; }

    [StringLength(20)]
    public string NumeroCuenta { get; set; } = null!;

    [StringLength(50)]
    public string TipoCuenta { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Saldo { get; set; }

    public int ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("Cuenta")]
    public virtual Cliente Cliente { get; set; } = null!;

    [InverseProperty("Cuenta")]
    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    [InverseProperty("CuentaDestino")]
    public virtual ICollection<Transferencium> TransferenciumCuentaDestinos { get; set; } = new List<Transferencium>();

    [InverseProperty("CuentaOrigen")]
    public virtual ICollection<Transferencium> TransferenciumCuentaOrigens { get; set; } = new List<Transferencium>();
}
