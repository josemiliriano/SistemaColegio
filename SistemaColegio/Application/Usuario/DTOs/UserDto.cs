using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Usuario.DTOs
{
    public class UserDto
    {        
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }

        // Datos de CDUser
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public char Activo { get; set; }

        // Datos del Role
        public string NombreRol { get; set; }
    }
}

