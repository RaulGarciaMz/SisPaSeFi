﻿using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScaffoldSSF.Models;
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
                          where u.IdUsuario == idUsuario && c.IdPunto > 0
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

        public async Task<List<EstadoPais>> ObtenerEstadosPaisAsync()
        {
            return await _catalogosConsultaContext.EstadosPais.ToListAsync();
        }

        public async Task<List<ProcesoResponsable>> ObtenerProcesosResponsablesAsync()
        {
            return await _catalogosConsultaContext.ProcesosResponsables.ToListAsync();
        }

        public async Task<List<TipoDocumento>> ObtenerTiposDocumentosAsync()
        {
            return await _catalogosConsultaContext.TiposDocumentos.ToListAsync();
        }

        public async Task<List<Municipio>> ObtenerMunicipiosPorEstadoAsync(int idEstado)
        {
            return await _catalogosConsultaContext.Municipios.Where(x => x.IdEstado == idEstado).ToListAsync();
        }

        public async Task<ProcesoResponsable?> ObtenerProcesosResponsablePorIdAsync(int id)
        {
            return await _catalogosConsultaContext.ProcesosResponsables.Where(x => x.IdProcesoResponsable == id).SingleOrDefaultAsync();
        }

        public async Task<List<CatalogoVista>> ObtenerCatalogoPorNombreTablaAync(string nombre)
        {
            var cat = "ssf." + nombre;

            string sqlQuery = @"SELECT id, CONCAT(nombre, ' - ' , clave) nombre
                                FROM @pTabla
                                ORDER BY id";

            object[] parametros = new object[]
            {
                new SqlParameter("@pTabla", cat),
            };

            return await _catalogosConsultaContext.CatalogosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        public async Task<List<ResultadoPatrullaje>> ObtenerResultadosPatrullajeAsync()
        {
            return await _catalogosConsultaContext.ResultadosPatrullaje.ToListAsync();
        }
    }
}