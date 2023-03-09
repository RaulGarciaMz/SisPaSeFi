using Domain.Entities;
using Domain.Ports.Driven.Repositories;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter
{
    public class CatalogoConsultasRepository : ICatalogosConsultaRepo
    {
        protected readonly CatalogosConsultaContext _catalogosConsultaContext;

        public CatalogoConsultasRepository(CatalogosConsultaContext catalogoContext)
        {
            _catalogosConsultaContext = catalogoContext ?? throw new ArgumentNullException(nameof(catalogoContext));
        }

        public async Task<List<ClasificacionIncidencia>> ObtenerClasificacionesIncidenciaAsync()
        {
            return await _catalogosConsultaContext.ClasificacionesIncidencia.ToListAsync();
        }

        public async Task<List<ComandanciaRegional>> ObtenerComandanciaPorIdUsuarioAsync(int idUsuario)
        {
            return await (from c in _catalogosConsultaContext.Comandancias
                          join u in _catalogosConsultaContext.Usuarios on c.IdUsuario equals u.IdUsuario
                          where u.IdUsuario == idUsuario
                          select c).ToListAsync();
        }

        public async Task<List<ConceptoAfectacion>> ObtenerConceptosAfectacionAsync()
        {
            return await _catalogosConsultaContext.ConceptosAfectacion.ToListAsync();
        }

        public async Task<List<Nivel>> ObtenerNivelesAsync()
        {
            return await _catalogosConsultaContext.Niveles.ToListAsync();
        }

        public async Task<List<int>> ObtenerRegionesMilitaresEnRutanAsync()
        {
            return await _catalogosConsultaContext.Rutas.Select(x => Convert.ToInt32(x.RegionMilitarSdn)).Distinct().ToListAsync();
        }

        public async Task<List<TipoPatrullaje>> ObtenerTiposPatrullajeAsync()
        {
            return await _catalogosConsultaContext.TiposPatrullaje.ToListAsync();
        }

        public async Task<List<TipoVehiculo>> ObtenerTiposVehiculoAsync()
        {
            return await _catalogosConsultaContext.TiposVehiculo.ToListAsync();
        }
    }
}