using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    [Table("Salas")]
    public class Salas
    {

        [Key]
        public int id { get; set; }
        

        //Se agregó la cantidad de lugares, ver en View de Create de la Sala como se ve para asignar el lugar. 
        public int CantidadLugares { get; set; }
    }
}
