using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Course
    {
        [Key]
        public int IdCurso { get; set; }
        public string Nombre { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
        public ICollection<CoursePeriod> CursoPeriodos { get; set; }
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>();
    }
}





