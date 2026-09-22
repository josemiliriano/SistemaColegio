using System;
using System.Collections.Generic;
using System.Text;

namespace Application.PeriodoAcademicoEstudiante.DTOs
{
    public class EstudentAcademicPeriodDto
    {
        public int IdStudentAcademicPeriod { get; set; }
        public int IdEstudiante { get; set; }
        public int IdPeriodo { get; set; }
        public int IdSessionPeriod { get; set; }
        public string Resultado { get; set; }
        public char Activo { get; set; }
    }
}
