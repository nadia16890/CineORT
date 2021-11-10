﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    
    public abstract class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreApellido { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Contrasenia { get; set; }

        public abstract TipoUsuario TipoUsuario { get; }

    }
}
