using Domain.DTOs;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IInicioPatrullajeCommand
    {
        Task AgregaInicioPatrullajeTransaccionalAsync(InicioPatrullajeDto a, int idUsuario, List<InicioPatrullajeProgramaVista> programas);
    }

    public interface IInicioPatrullajeQuery
    {
       Task<List<InicioPatrullajeProgramaVista>> ObtenerProgramaPorRutaAndFechaAsync(int idRuta, string fecha);
    }
}
