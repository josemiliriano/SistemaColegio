using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Estudent
    {
        [Key]
        public int IdEstudiante { get; set; }
        public int IdPersona { get; set; }
        public int CodigoEstudiante { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
    }
}
