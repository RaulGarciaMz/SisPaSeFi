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
    public class PersonalParticipanteContext : DbContext
    {
        public PersonalParticipanteContext(DbContextOptions<PersonalParticipanteContext> options)
        : base(options)
        {
        }

        public DbSet<UsuarioPatrullaje> UsuariosPatrullaje { get; set; }
        public DbSet<PersonalParticipanteVista> PersonasParticipantesVista { get; set; }
    }
}
