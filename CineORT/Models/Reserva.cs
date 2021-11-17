using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    [Table("Reservas")]
    public class Reserva
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdFuncion { get; set; }

        public int CantidadEntradas { get; set; }
    }
}
