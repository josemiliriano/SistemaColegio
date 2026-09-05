using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Estudiante.DTOs
{
    public class EstudentDto
    {
        public int IdEstudiante { get; set; }
        public int IdPersona { get; set; }
        public int CodigoEstudiante { get; set; }
        public char Activo { get; set; }
    }
}
