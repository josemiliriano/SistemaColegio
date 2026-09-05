using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Profesor.DTOs
{
    public class ProfesorDto
    {
        // Datos de Person
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }

        // Datos de Professor
        public string Cedula { get; set; }
        public string Especialidad { get; set; }
        public char Activo { get; set; }

        // Datos de acceso
        public string NombreUsuario { get; set; }
    }
}

