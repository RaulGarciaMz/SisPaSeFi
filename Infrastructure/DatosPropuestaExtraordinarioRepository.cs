using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class DatosPropuestaExtraordinarioRepository : IDatosPropuestaExtraRepo
    {
        protected readonly DatosPropuestaExtraordinariaContext _propuestaContext;

        public DatosPropuestaExtraordinarioRepository(DatosPropuestaExtraordinariaContext propuestaContext)
        {
            _propuestaContext = propuestaContext ?? throw new ArgumentNullException(nameof(propuestaContext));
        }

        public async Task<List<UbicacionPropuestaExtraVista>> ObtenerUbicacionLineasPorIdPropuestaAsync(int idPropuesta)
        {
            string sqlQuery = @"SELECT DISTINCT c.clave, d.ubicacion inicio, e.ubicacion fin 
                                FROM ssf.propuestasPatrullajes a
                                JOIN ssf.propuestasPatrullajesLineas b ON a.id_propuestaPatrullaje=b.id_propuestaPatrullaje
                                JOIN ssf.linea c ON b.id_linea=c.id_linea
                                JOIN ssf.puntospatrullaje d ON c.id_punto_inicio=d.id_punto
                                JOIN ssf.puntospatrullaje e ON c.id_punto_fin=e.id_punto
                                WHERE a.id_propuestaPatrullaje= @pIdPropuesta";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdPropuesta", idPropuesta)
            };

            return await _propuestaContext.UbicacionesPropVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<VehiculoPropuestaExtraVista>> ObtenerVehiculosPorIdPropuestaAsync(int idPropuesta)
        {
            string sqlQuery = @"SELECT c.matricula placas, c.numeroEconomico numero, d.descripciontipoVehiculo tipo 
                                FROM ssf.propuestaspatrullajes a
                                JOIN ssf.propuestaspatrullajesvehiculos b ON a.id_propuestaPatrullaje=b.id_propuestaPatrullaje
                                JOIN ssf.vehiculos c ON  b.id_vehiculo=c.id_vehiculo
                                JOIN ssf.tipovehiculo d ON c.id_tipoVehiculo=d.id_tipoVehiculo
                                WHERE a.id_propuestaPatrullaje= @pIdPropuesta";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdPropuesta", idPropuesta)
            };

            return await _propuestaContext.VehiculosPropVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<UsuarioPropuestaExtraVista>> ObtenerResponsablesPorIdPropuestaAsync(int idPropuesta)
        {
            string sqlQuery = @"SELECT a.fechaPatrullaje, b.fechaTermino, e.ubicacion, f.nombre municipio, g.nombre estado,
                                       h.nombre, h.apellido1, h.apellido2, i.ubicacion InstalacionResponsable,
                                       j.nombre mun, k.nombre edo 
                                FROM ssf.propuestaspatrullajes a
                                JOIN ssf.propuestaspatrullajescomplementoSSF b ON a.id_propuestaPatrullaje=b.id_propuestaPatrullaje
                                JOIN ssf.rutas c ON a.id_ruta=c.id_ruta
                                JOIN ssf.comandanciasregionales d ON c.regionSSF=d.numero
                                JOIN ssf.puntosPatrullaje e ON d.id_punto=e.id_punto
                                JOIN ssf.municipios f ON e.id_municipio=f.id_municipio
                                JOIN ssf.estadosPais g ON f.id_estado=g.id_estado
                                JOIN ssf.usuarios h ON d.id_usuario=h.id_usuario
                                JOIN ssf.puntosPatrullaje i ON a.id_puntoResponsable=i.id_punto
                                JOIN ssf.municipios j ON i.id_municipio=j.id_municipio
                                JOIN ssf.estadosPais k ON j.id_estado=k.id_estado
                                WHERE a.id_propuestaPatrullaje= @pIdPropuesta";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdPropuesta", idPropuesta)
            };

            return await _propuestaContext.UsuariosPropVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }
    }
}


 