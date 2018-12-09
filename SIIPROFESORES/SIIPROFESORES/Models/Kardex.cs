
using System;
using System.Collections.Generic;
using System.Text;

namespace SIIPROFESORES.Models
{
    class Kardex
    {
        public Matter matter{get;set;}
        public Oportunity oportunity { get; set; }
        public int qualification { get; set; }
        public int semester { get; set; }
        public Student student { get; set; }
    }
}
