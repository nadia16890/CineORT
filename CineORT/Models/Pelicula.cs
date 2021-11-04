using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    public class Pelicula
    {

        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

       

    }
}
