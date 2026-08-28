using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Period
    {
        [Key]
        public int IdPeriodo { get; set; }
        public string Nombre { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaFin { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
    }
}
