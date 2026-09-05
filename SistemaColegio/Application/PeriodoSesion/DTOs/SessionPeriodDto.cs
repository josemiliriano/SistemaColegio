using System;
using System.Collections.Generic;
using System.Text;

namespace Application.PeriodoSesion.DTOs
{
    public class SessionPeriodDto
    {
        public int IdSessionPeriod { get; set; }
        public int IdSeccion { get; set; }
        public int IdPeriodo { get; set; }
        public int IdAula { get; set; }
        public char IsDelete { get; set; }
    }
}
