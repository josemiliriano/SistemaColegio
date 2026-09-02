using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Classroom
    {
        [Key]
        public int IdAula { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public int Capacidad { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
        public ICollection<SessionPeriod> SessionPeriods { get; set; } = new List<SessionPeriod>();
    }
}
