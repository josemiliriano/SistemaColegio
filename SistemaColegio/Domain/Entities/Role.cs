using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Role
    {
        [Key]
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
        public ICollection<CDUser> Users { get; set; }
    }
}
