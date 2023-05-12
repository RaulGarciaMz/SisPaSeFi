using Domain.Entities;

namespace Domain.Ports.Driven
{
    public interface IRegistroCommand
    {
        Task AumentaIntentosDeUsuarioEnMemoriaAsync(string usuario);
        Task ReseteaIntentosDeUsuarioEnMemoriaAsync(string usuario);
        Task<bool> BloqueaUsuarioEnMemoriaAsync(string usuario);
        Task RegistraEventoDeUsuarioAsync(int numSesion, string resultado);
        Task RegistraFinDeSesionAsync(int numSesion);       
        Task ActualizaTotalDeAccesosEnMemoriaAsync(int accesos);
        void AgregaAccesoEnMemoria();
        Task ActualizaUltimoAccesoDeUsuarioEnMemoriaAsync(string usuario);
        Task AgregaSesionDeUsuarioEnMemoriaAsync(string usuario);
        Task ActualizaAvisoLegalDeUsuarioAsync(string usuario);
        Task ActualizaCorreoElectronicoDeUsuarioAsync(string usuario, string correo, int notificar);
        Task<bool> SaveChangesAsync();
    }

    public interface IRegistroQuery
    {
        string ObtenerPaginaDeInicioDeUsuario(int idUsuario);
        Task<int?> ObtenerAvisoLegalDeUsuarioAsync(string usuario);
        Task<List<Acceso>> ObtenerAccesosAsync();
        Task<int> ObtenerUltimaSesionDeUsuarioAsync(int idUsuario);
    }
}
