using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LAB3.Models
{
    public class Farmaco
    {
        public int Id { get; set; }
        [Required]
        public string Nombre_Farmaco { get; set; }
        [Required]
        public string Descripción_Farmaco { get; set; }
        [Required]
        public string Casa_Productora { get; set; }
        [Required]
        public int Existencia { get; set; }
        [Required]
        public double Precio { get; set; }
        public static int Compare_Nombre_Farmaco(Farmaco x, Farmaco y) //Funcion del delegado
        {
            int r = x.Nombre_Farmaco.CompareTo(y.Nombre_Farmaco);
            return r;
        }

    }
}
