using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AsignacionDocente.DTOs
{
    public class TeachingAssignmentDto
    {
        public int IdAsignacionDocente { get; set; }
        public int IdProfesorMateria { get; set; }
        public int IdSessionPeriod { get; set; }
        public char Activo { get; set; }
    }
}
