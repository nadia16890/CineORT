using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    [Table("Funcion")]
    public class Funcion

    {
        [Key]
        public int Id { get; set; }

        public int PeliculaId { get; set; }

        public string Fecha { get; set; }

        public string Horario { get; set; }

        public int Sala { get; set; }

        public string NombrePelicula { get; set; }

    }
}
