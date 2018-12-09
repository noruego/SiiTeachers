using System;
using System.Collections.Generic;
using System.Text;

namespace SIIPROFESORES.Models
{
    class Subjects
    {
        public Group group { get; set; }
        public Student student { get; set; }
        public String period { get; set; }
        public int calificacion1 { get; set; }
        public int calificacion2 { get; set; }
        public int calificacion3 { get; set; }
        public int calificacion4 { get; set; }
        public int promedio { get; set; }
    }
}
