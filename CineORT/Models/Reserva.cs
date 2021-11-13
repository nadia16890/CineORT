﻿using System;
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
        public string Usuario { get; set; }
        public int IdFuncion { get; set; }

        public int CantidadEntradas { get; set; }
    }
}
