using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace idatbancoapi.Models;

public partial class Transferencium
{
    [Key]
    public int TransferenciaId { get; set; }

    public int CuentaOrigenId { get; set; }

    public int CuentaDestinoId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Monto { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [ForeignKey("CuentaDestinoId")]
    [InverseProperty("TransferenciumCuentaDestinos")]
    public virtual Cuentum CuentaDestino { get; set; } = null!;

    [ForeignKey("CuentaOrigenId")]
    [InverseProperty("TransferenciumCuentaOrigens")]
    public virtual Cuentum CuentaOrigen { get; set; } = null!;
}
