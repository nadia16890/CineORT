using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    [Table("Cliente")]
    public class Cliente : Usuario
    {
        public override TipoUsuario TipoUsuario => TipoUsuario.Cliente;
    }
}
