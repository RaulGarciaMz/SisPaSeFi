using Domain.Ports.Driven.Repositories;
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
            return await _ubicacionContext.Programas.Where(x => x.IdRuta == idRuta && x.FechaPatrullaje == fecha).Select(x => x.IdPrograma).SingleOrDefaultAsync();
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
