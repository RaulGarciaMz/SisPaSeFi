using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class UbicacionPatrullajeRepository : IUbicacionPatrullajeRepo
    {

        protected readonly UbicacionPatrullajeContext _ubicacionContext;

        public UbicacionPatrullajeRepository(UbicacionPatrullajeContext ubicacionContext)
        {
            _ubicacionContext = ubicacionContext ?? throw new ArgumentNullException(nameof(ubicacionContext));
        }

        public async Task<int?> ObtenerIdProgramaPorRutaAndFechaAsync(int idRuta, DateTime fecha)
        {
            string sqlQuery = @"SELECT a.id_programa, a.riesgopatrullaje, b.regionSSF 
                                FROM ssf.programapatrullajes a
								JOIN ssf.rutas b ON a.id_ruta=b.id_ruta
                                WHERE a.id_ruta= @pIdRuta
                                AND a.fechapatrullaje= @pFecha
                                AND a.id_estadopatrullaje < (SELECT id_estadopatrullaje FROM SSF.estadopatrullaje WHERE descripcionestadopatrullaje='Concluido')";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdRuta", idRuta),
                new SqlParameter("@pFecha", fecha)
            };

            var prog = await _ubicacionContext.ProgramasRegionVista.FromSqlRaw(sqlQuery, parametros).FirstOrDefaultAsync();
            if (prog == null) 
            {
                return null;
            }

            return prog.id_programa;
        }


        public async Task ActualizarUbicacionAsync(int idPrograma, int idUsuario, string latitud, string longitud)
        {
            var prog = await _ubicacionContext.Programas.Where(x => x.IdPrograma == idPrograma).SingleOrDefaultAsync();

            prog.UltimaActualizacion = DateTime.UtcNow;
            prog.Latitud = latitud;
            prog.Longitud = longitud;
            prog.IdUsuario = idUsuario;

            _ubicacionContext.Programas.Update(prog);

            await _ubicacionContext.SaveChangesAsync();
        }
    }
}
