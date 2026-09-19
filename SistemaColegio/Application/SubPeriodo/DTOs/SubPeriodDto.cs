using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SubPeriodo.DTOs
{
    public class SubPeriodDto
    {
        public int IdSubPeriodo { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public char Activo { get; set; }
    }
}
