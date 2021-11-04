using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    public class Salas
    {

        [Key]
        public int id { get; set; }
        
        public int CantidadLugares { get; set; }
    }
}
