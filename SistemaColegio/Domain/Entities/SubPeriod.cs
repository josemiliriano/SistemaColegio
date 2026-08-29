using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class SubPeriod
    {
        [Key]
        public int IdSubPeriodo { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';        
        public ICollection<AcademicSubPeriod> AcademicSubPeriods { get; set; } = new List<AcademicSubPeriod>();
    }
}
