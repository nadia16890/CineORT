using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineORT.Models;

namespace CineORT.Models
{
    
    
        public class CineDbContext : DbContext
        {
            public CineDbContext(DbContextOptions opciones) : base(opciones)
            {

            }

        
        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Administrador> Administradors { get; set; }

        public DbSet<CineORT.Models.Reserva> Reserva { get; set; }

        public DbSet<CineORT.Models.Pelicula> Pelicula { get; set; }

        public DbSet<CineORT.Models.Funcion> Funcion { get; set; }

        public DbSet<CineORT.Models.Salas> Salas { get; set; }


    }

    }
