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

        
        public DbSet<Usuario> Usuarios{ get; set; }

        public DbSet<Administrador> Administrador { get; set; }

        public DbSet<Reserva> Reserva { get; set; }

        public DbSet<Pelicula> Pelicula { get; set; }

        public DbSet<Funcion> Funcion { get; set; }

        public DbSet<Salas> Salas { get; set; }


    }

    }
