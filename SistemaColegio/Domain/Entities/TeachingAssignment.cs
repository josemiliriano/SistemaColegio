using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class TeachingAssignment
    {
        [Key]
        public int IdAsignacionDocente { get; set; }
        public int IdProfesorMateria { get; set; }
        public int IdSessionPeriod { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
        public ProfessorSubject ProfessorSubject { get; set; }
        public SessionPeriod SessionPeriod { get; set; }
    }
}
