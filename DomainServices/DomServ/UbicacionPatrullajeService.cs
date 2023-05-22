using Domain.DTOs;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class UbicacionPatrullajeService : IUbicacionPatrullajeService
    {
        private readonly IUbicacionPatrullajeRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public UbicacionPatrullajeService(IUbicacionPatrullajeRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task ActualizaUbicacionAsync(UbicacionForUpdateDto ubicacion)
        {
            var userId = await _user.ObtenerIdUsuarioPorUsuarioNomAsync(ubicacion.usuario);

            if (userId != null) 
            {
                var fechaPat = DateTime.Parse(ubicacion.FechaPatrullaje);
                var ruta = Int32.Parse(ubicacion.IdRuta);

                var idPrograma = await _repo.ObtenerIdProgramaPorRutaAndFechaAsync(ruta, fechaPat);

                if (idPrograma != null) 
                {
                    await _repo.ActualizarUbicacionAsync(idPrograma.Value, userId.Value, ubicacion.Latitud, ubicacion.Longitud);
                }
            }
        }
    }
}
