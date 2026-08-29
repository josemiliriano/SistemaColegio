using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Month
    {
        [Key]
        public int IdMes { get; set; }
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public char Activo { get; set; } = '1';
        public char IsDelete { get; set; } = '0';        
        public ICollection<AcademicMonth> AcademicMonths { get; set; } = new List<AcademicMonth>();
    }
}
