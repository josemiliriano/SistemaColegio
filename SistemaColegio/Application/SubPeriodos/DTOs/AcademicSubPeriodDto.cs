using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.SubPeriodos.DTOs
{
    public class AcademicSubPeriodDto
    {
        public int IdPeriodoAcademico { get; set; }
        public int IdPeriodo { get; set; }
        public int IdSubPeriodo { get; set; }        
        public DateTime FechaInicio { get; set; }        
        public DateTime FechaFin { get; set; }
        public char Activo { get; set; } = '1';
    }
}
