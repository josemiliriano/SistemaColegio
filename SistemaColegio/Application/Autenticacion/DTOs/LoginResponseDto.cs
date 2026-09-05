using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Autenticacion.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public string NombreUsuario { get; set; }
        public string NombreRol { get; set; }
    }
}
