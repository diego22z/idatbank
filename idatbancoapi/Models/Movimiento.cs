using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace idatbancoapi.Models;

[Table("Movimiento")]
public partial class Movimiento
{
    [Key]
    public int MovimientoId { get; set; }

    public int CuentaId { get; set; }

    [StringLength(50)]
    public string TipoMovimiento { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Monto { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [ForeignKey("CuentaId")]
    [InverseProperty("Movimientos")]
    public virtual Cuentum? Cuenta { get; set; }
}
