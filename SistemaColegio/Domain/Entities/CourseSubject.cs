using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class CourseSubject
    {
        [Key]
        public int IdCursoMateria { get; set; }
        public int IdCurso { get; set; }
        public int IdMateria { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
        public Course Course { get; set; }
        public Subject Subject { get; set; }
    }
}
