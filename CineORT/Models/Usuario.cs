﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    public class Usuario
    {
        
        public int Id { get; set; }

        public string NombreApellido { get; set; }

        public string Email { get; set; }

        public string Contraseña { get; set; }

    }
}
