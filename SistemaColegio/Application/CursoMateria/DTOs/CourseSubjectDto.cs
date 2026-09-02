using System;
using System.Collections.Generic;
using System.Text;

namespace Application.CursoMateria.DTOs
{
    public class CourseSubjectDto
    {
        public int IdCurso { get; set; }
        public int IdMateria { get; set; }
        public char Activo { get; set; }
    }
}
