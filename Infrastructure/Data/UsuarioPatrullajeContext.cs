using Domain.Entities;
using Domain.Entities.Vistas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter.Data
{
    public class UsuarioPatrullajeContext : DbContext
    {
        public UsuarioPatrullajeContext(DbContextOptions<UsuarioPatrullajeContext> options)
        : base(options)
        {
        }

        public DbSet<UsuarioPatrullaje> UsuariosPatrullaje { get; set; }
        public DbSet<UsuarioPatrullajeVista> UsuariosPatrullajeVista { get; set; }
    }
}
