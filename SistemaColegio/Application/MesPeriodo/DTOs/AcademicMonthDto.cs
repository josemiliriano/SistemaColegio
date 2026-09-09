using System;
using System.Collections.Generic;
using System.Text;

namespace Application.MesPeriodo.DTOs
{
    public class AcademicMonthDto
    {
        public int IdMesAcademico { get; set; }
        public int IdPeriodoAcademico { get; set; }
        public int IdMes { get; set; }
        public int Orden { get; set; }
        public char Activo { get; set; }
    }
}
