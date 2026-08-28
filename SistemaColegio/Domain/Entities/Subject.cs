using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Subject
    {
        [Key]
        public int IdMateria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
    }
}
