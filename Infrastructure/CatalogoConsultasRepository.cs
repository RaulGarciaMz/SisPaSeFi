using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

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
                          join u in _catalogosConsultaContext.UsuariosComandancia on c.IdComandancia equals u.IdComandancia
                          where u.IdUsuario == idUsuario && c.IdPunto > 0
                          select c).ToListAsync();
        }


        public async Task<List<ComandanciaRegional>> ObtenerComandanciasAsync()
        {
            return await _catalogosConsultaContext.Comandancias.Where(x => x.IdPunto > 0).ToListAsync();
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

        public async Task<List<EstadoPatrullaje>> ObtenerEstadosPatrullajeAsync()
        {
            return await _catalogosConsultaContext.EstadosPatrullaje.Where(x => x.IdEstadoPatrullaje > 4).ToListAsync();
        }

        public async Task<List<ApoyoPatrullaje>> ObtenerApoyosPatrullajeAsync()
        {
            return await _catalogosConsultaContext.ApoyosPatrullaje.ToListAsync();
        }

        public async Task<List<PuntoPatrullaje>> ObtenerInstalacionesDeComandanciaAsync(int idComandancia)
        {
            return await _catalogosConsultaContext.PuntosPatrullaje.Where(x => x.EsInstalacion == 1 && x.IdComandancia == idComandancia).ToListAsync();
        }

        public async Task<List<NivelRiesgo>> ObtenerNivelDeRiesgoAsync()
        {
            return await _catalogosConsultaContext.NivelesRiesgo.ToListAsync();
        }

        public async Task<List<CatalogoHallazgo>> ObtenerHallazgosAsync()
        {
            return await _catalogosConsultaContext.Hallazgos.ToListAsync();
        }

        public async Task<List<Localidad>> ObtenerLocalidadesPorMunicipioAsync(int idMunicipio)
        {
            return await _catalogosConsultaContext.Localidades.Where(x => x.IdMunicipio == idMunicipio).OrderBy(x => x.Nombre).ToListAsync();
        }

        public async Task<List<EstadoIncidencia>> ObtenerEstadosIncidenciaAsync()
        {
            return await _catalogosConsultaContext.EstadosIncidencia.OrderBy(x => x.IdEstadoIncidencia).ToListAsync();
        }

        public async Task<List<UsuarioComandancia>> ObtenerComandanciasDeUnUsuarioAsync(int idMunicipio)
        {
            return await _catalogosConsultaContext.UsuariosComandancia.Where(x => x.IdUsuario == idMunicipio).Distinct().ToListAsync();
        }

        public async Task<List<UsuarioGrupoCorreoElectronico>> ObtenerGruposCorreoDeUnUsuarioAsync(int idUsuario)
        {
            return await _catalogosConsultaContext.UsuariosGrupoCorreoElectronico.Where(x => x.IdUsuario == idUsuario).Distinct().ToListAsync();
        }

        public async Task<List<UsuarioRol>> ObtenerRolesDeUnUsuarioAsync(int idUsuario)
        {
            return await _catalogosConsultaContext.UsuariosRoles.Where(x => x.IdUsuario == idUsuario).Distinct().ToListAsync();
        }

        public async Task<List<GrupoCorreoElectronico>> ObtenerGruposCorreoAsync()
        {
            return await _catalogosConsultaContext.GruposCorreoElectronico.OrderBy(x => x.IdGrupoCorreo).ToListAsync();
        }

        public async Task<List<RolMenu>> ObtenerMenusDeRolAsync(int idRol)
        {
            return await _catalogosConsultaContext.RolesMenu.Where(x => x.IdRol == idRol).Distinct().ToListAsync();
        }
        
    }
}