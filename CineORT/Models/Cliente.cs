using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    public class Cliente : Usuario
    {
        public override TipoUsuario TipoUsuario => TipoUsuario.Cliente;
    }
}
