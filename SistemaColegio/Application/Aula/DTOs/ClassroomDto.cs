using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Aula.DTOs
{
    public class ClassroomDto
    {
        public int IdAula { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public int Capacidad { get; set; }
        public char Activo { get; set; }
    }
}
