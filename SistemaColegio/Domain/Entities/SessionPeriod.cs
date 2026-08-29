using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class SessionPeriod
    {
        [Key]
        public int IdSessionPeriod { get; set; }
        public int IdSeccion { get; set; }
        public int IdPeriodo { get; set; }
        public int IdAula { get; set; }
        public Session Session { get; set; }
        public Period Period { get; set; }
        public Classroom Classroom { get; set; }
    }
}
