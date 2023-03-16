using Domain.Entities;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter
{
    public class AfectacionIncidenciaRepository
    {
        protected readonly AfectacionIncidenciasContext _afectacionContext;

        public AfectacionIncidenciaRepository(AfectacionIncidenciasContext afectacionContext)
        {
            _afectacionContext = afectacionContext ?? throw new ArgumentNullException(nameof(afectacionContext));
        }

        public async Task<List<AfectacionIncidencia>> ObtenerAfectacionIncidenciaAsync()
        {
           var p = new List<AfectacionIncidencia>();

            return p;
        }

    }
}
