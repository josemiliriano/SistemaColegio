using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Periodo.DTOs
{
    public class PeriodDto
    {
        public int IdPeriodo { get; set; }
        public string Nombre { get; set; }        
        public DateTime FechaInicio { get; set; }        
        public DateTime FechaFin { get; set; }
        public char Activo { get; set; } 
    }
}
