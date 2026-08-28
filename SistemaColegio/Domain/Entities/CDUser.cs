using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class CDUser
    {
        [Key]
        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
    }
}
