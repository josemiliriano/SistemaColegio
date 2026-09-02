using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Professor
    {
        [Key]
        public int IdProfesor { get; set; }
        public int IdPersona { get; set; }
        public string Cedula { get; set; }
        public string Especialidad { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';

        // Navegación hacia Person
        public Person Persona { get; set; }

        // Relación con las materias
        public ICollection<ProfessorSubject> ProfessorSubjects { get; set; }
    }
}