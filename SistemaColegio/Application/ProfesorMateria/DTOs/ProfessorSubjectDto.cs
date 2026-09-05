using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ProfesorMateria.DTOs
{
    public class ProfessorSubjectDto
    {
        public int IdProfesorMateria { get; set; }
        public int IdProfesor { get; set; }
        public int IdMateria { get; set; }
        public char Activo { get; set; }
    }
}
