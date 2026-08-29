using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class ProfessorSubject
    {
        [Key]
        public int IdProfesorMateria { get; set; }
        public int IdProfesor { get; set; }
        public int IdMateria { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
        public Professor Professor { get; set; }
        public Subject Subject { get; set; }
    }
}
