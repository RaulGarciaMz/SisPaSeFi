using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class OficioAutorizacionRepository : IOficioAutorizacionRepo
    {
        protected readonly OficioAutorizacionContext _oficioContext;

        public OficioAutorizacionRepository(OficioAutorizacionContext oficioContext)
        {
            _oficioContext = oficioContext ?? throw new ArgumentNullException(nameof(oficioContext));
        }

        public async Task<List<PropuestaConResponsableVista>> ObtenerPropuestaConResponsableAsync(int idPropuesta)
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
                                WHERE a.id_propuestaPatrullaje= @pIdPropuesta ";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdPropuesta", idPropuesta)
            };

            return await _oficioContext.Propuestas.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        public async Task<List<LineaEnPropuestaVista>> ObtenerLineasDePropuestaAsync(int idPropuesta)
        {
            string sqlQuery = @"SELECT DISTINCT c.clave,d.ubicacion inicio,e.ubicacion fin 
                                FROM ssf.propuestasPatrullajes a
                                JOIN ssf.propuestasPatrullajesLineas b ON a.id_propuestaPatrullaje=b.id_propuestaPatrullaje
                                JOIN ssf.linea c ON  b.id_linea=c.id_linea
                                JOIN ssf.puntospatrullaje d ON c.id_punto_inicio=d.id_punto
                                JOIN ssf.puntospatrullaje e ON c.id_punto_fin=e.id_punto
                                WHERE a.id_propuestaPatrullaje = @pIdPropuesta";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdPropuesta", idPropuesta)
            };

            return await _oficioContext.LineasEnPropuestaVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }

        public async Task<List<VehiculoEnPropuestaVista>> ObtenerVehiculosEnPropuestaAsync(int idPropuesta)
        {
            string sqlQuery = @"SELECT c.matricula placas,c.numeroEconomico numero,d.descripciontipoVehiculo tipo 
                                FROM ssf.propuestaspatrullajes a
                                JOIN ssf.propuestaspatrullajesvehiculos b ON a.id_propuestaPatrullaje=b.id_propuestaPatrullaje
                                JOIN ssf.vehiculos c ON b.id_vehiculo=c.id_vehiculo
                                JOIN ssf.tipovehiculo d ON c.id_tipoVehiculo=d.id_tipoVehiculo
                                WHERE a.id_propuestaPatrullaje= @pIdPropuesta";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdPropuesta", idPropuesta)
            };

            return await _oficioContext.VehiculosEnPropuesta.FromSqlRaw(sqlQuery, parametros).ToListAsync();

        }
    }
}
