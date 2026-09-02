using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entities
{
    public class Session
    {
        [Key]
        public int IdSeccion { get; set; }
        public int IdCurso { get; set; }
        public string Nombre { get; set; }
        public int CupoCapacidadMaximo { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';
        public Course Course { get; set; }
        public ICollection<SessionPeriod> SessionPeriods { get; set; } = new List<SessionPeriod>();

    }
}


