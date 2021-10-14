using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineORT.Models
{
    
    
        public class CineDbContext : DbContext
        {
            public CineDbContext(DbContextOptions opciones) : base(opciones)
            {

            }
            public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Administrador> Administradors { get; set; }


    }

    }
