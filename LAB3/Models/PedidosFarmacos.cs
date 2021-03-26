using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LAB3.Models
{
    public class PedidosFarmacos
    {
        public int Id { get; set; }
        [Required]
        public string NombreDelCliente { get; set; }
        [Required]
        public string Dirrecion { get; set; }
        [Required]
        public int Nit { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
