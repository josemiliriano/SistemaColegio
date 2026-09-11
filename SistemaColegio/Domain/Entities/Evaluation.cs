using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Evaluation
    {
        [Key]
        public int IdEvaluacion { get; set; }

        public int IdEstudiante { get; set; }
        public int IdAsignacionDocente { get; set; }
        public int IdSubPeriodo { get; set; }

        public decimal Asistencia { get; set; }
        public decimal Tarea { get; set; }
        public decimal Cuaderno { get; set; }
        public decimal Participacion { get; set; }
        public decimal Proyecto { get; set; }
        public decimal Exposicion { get; set; }
        public decimal Examen { get; set; }

        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';

        public Estudent Estudent { get; set; }
        public TeachingAssignment TeachingAssignment { get; set; }
        public SubPeriod SubPeriod { get; set; }
    }
}
