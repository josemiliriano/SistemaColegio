using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class AcademicSubPeriod
    {
        [Key]
        public int IdPeriodoAcademico { get; set; }
        public int IdPeriodo { get; set; }
        public int IdSubPeriodo { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }
        [Column(TypeName = "date")]
        public DateTime FechaFin { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';        
        public Period Period { get; set; }
        public SubPeriod SubPeriod { get; set; }
        public ICollection<AcademicMonth> AcademicMonths { get; set; } = new List<AcademicMonth>();
    }
}
