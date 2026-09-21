using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class EstudentAcademicPeriod
    {
        [Key]
        public int IdStudentAcademicPeriod { get; set; }
        public int IdEstudiante { get; set; }
        public int IdPeriodo { get; set; }
        public int IdSessionPeriod { get; set; }
        public string Resultado { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
        public Estudent Estudent { get; set; }
        public Period Period { get; set; }
        public SessionPeriod SessionPeriod { get; set; }
    }
}
