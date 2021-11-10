using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{

    [Table ("Administrador")]
    public class Administrador : Usuario
    {

        public override TipoUsuario TipoUsuario => TipoUsuario.Administrador;
        // [Key]
        // public int Id { get; set; }

        // [Required]
        //public string Email { get; set; }

        // [Required]
        // public string Contrasenia { get; set; }

    }
}
