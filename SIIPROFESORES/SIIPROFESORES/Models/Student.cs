using System;
using System.Collections.Generic;
using System.Text;

namespace SIIPROFESORES.Models
{
    class Student
    {
        public String email { get; set; }
        public String father_lastname { get; set; }
        public String mother_lastname { get; set; }
        public String nocontrol { get; set; }
        public String name { get; set; }
        public String carrera { get; set; }
        public int idstudent { get; set; }
        public Career career { get; set; }
        public String image { get; set; }
    }
}
