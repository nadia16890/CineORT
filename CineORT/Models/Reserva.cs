using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Funcion Funcion { get; set; }
    }
}
