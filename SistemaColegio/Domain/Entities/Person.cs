using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Person
    {
        [Key]
        public int IdPersona { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }        
        [Column(TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
        public CDUser Usuario { get; set; }
        public Estudent Estudiante { get; set; }
        public Professor Profesor { get; set; }
    }
}
