using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Seccion.DTOs
{
    public class SessionDto
    {
        public int IdSeccion { get; set; }
        public int IdCurso { get; set; }
        public string Nombre { get; set; }
        public int CupoCapacidadMaximo { get; set; }
        public char Activo { get; set; }
    }
}
