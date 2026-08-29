using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class CoursePeriod
    {
        [Key]
        public int IdCursoPeriodo { get; set; }        
        public int IdCurso { get; set; }        
        public int IdPeriodo { get; set; }        
        public Course Course { get; set; }
        public Period Period { get; set; }
    }
}
