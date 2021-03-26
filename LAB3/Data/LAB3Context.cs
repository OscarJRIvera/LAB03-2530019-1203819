using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LAB3.Models;

namespace LAB3.Data
{
    public class LAB3Context : DbContext
    {
        public LAB3Context (DbContextOptions<LAB3Context> options)
            : base(options)
        {
        }

        public DbSet<LAB3.Models.Farmaco> Farmaco { get; set; }

        public DbSet<LAB3.Models.PedidosFarmacos> PedidosFarmacos { get; set; }
    }
}
