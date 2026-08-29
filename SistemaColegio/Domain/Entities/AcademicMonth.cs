using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class AcademicMonth
    {
        [Key]
        public int IdMesAcademico { get; set; }
        public int IdPeriodoAcademico { get; set; }
        public int IdMes { get; set; }
        public int Orden { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';        
        public AcademicSubPeriod AcademicSubPeriod { get; set; }
        public Month Month { get; set; }
    }
}
