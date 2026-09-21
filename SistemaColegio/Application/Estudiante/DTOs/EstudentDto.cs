using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Estudiante.DTOs
{
    public class EstudentDto
    {
        public int IdEstudiante { get; set; }

        public int IdPersona { get; set; }

        // Datos de la Persona
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }        

        // Datos del Estudiante
        public int CodigoEstudiante { get; set; }
        public int IdSessionPeriod { get; set; }

        public char Activo { get; set; }
    }
}
