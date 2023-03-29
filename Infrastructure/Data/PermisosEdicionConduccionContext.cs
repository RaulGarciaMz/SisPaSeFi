using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter.Data
{
    public class PermisosEdicionConduccionContext : DbContext
    {
        public PermisosEdicionConduccionContext(DbContextOptions<PermisosEdicionConduccionContext> options)
        : base(options)
        {
        }

        public DbSet<Permisosedicionprocesoconduccion> PermisosEdicionConduccion { get; set; }
    }
}
