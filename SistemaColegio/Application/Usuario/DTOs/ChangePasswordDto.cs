using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Usuario.DTOs
{
    public class ChangePasswordDto
    {
        public string PasswordActual { get; set; }
        public string NuevaPassword { get; set; }
    }
}
