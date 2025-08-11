using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace idatbancoapi.Models;

[Table("Cliente")]
[Index("Dni", Name = "UQ__Cliente__C035B8DDD7EDBD33", IsUnique = true)]
public partial class Cliente
{
    [Key]
    public int ClienteId { get; set; }

    [StringLength(100)]
    public string Nombres { get; set; } = null!;

    [StringLength(100)]
    public string Apellidos { get; set; } = null!;

    [Column("DNI")]
    [StringLength(15)]
    public string Dni { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [InverseProperty("Cliente")]
    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
