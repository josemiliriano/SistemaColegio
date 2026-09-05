using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Usuario.DTOs
{
    public class CreateUserDto
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }

        public string NombreUsuario { get; set; }
        public string Password { get; set; }

        public int IdRol { get; set; }
    }
}
