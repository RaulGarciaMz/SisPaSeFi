using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IUsuariosDtoCommand
    {
/*        Task BloqueaUsuarioAsync(string usuario);
        Task DesbloqueaUsuarioAsync(string usuario);
        Task ReiniciaClaveUsuarioAsync(string usuario);*/
        Task ActualizaUsuariosPorOpcionAsync(string opcion, string usuario, List<UsuarioDtoForUpdate> users);
        Task AgregaPorOpcionAsync(string opcion, string dato, string usuario, UsuarioDto useDto);
        Task BorraPorOpcionAsync(string opcion, string dato, string usuario);
    }

    public interface IUsuariosDtoQuery
    {
        Task<List<UsuarioDtoForGetListas>> ObtenerUsuarioPorOpcionAsync(string opcion, string criterio, string usuario);
        //Task<UsuarioDto?> ObtenerUsuarioPorCriterioAsync(string criterio);
        //Task<List<UsuarioDto>> ObtenerUsuariosPorCriterioAsync(string criterio);
        // Task<List<UsuarioDto>> ObtenerUsuarioPorOpcionAsync(string opcion, string criterio, string usuario);

        //Métodos comunes para ser usados por otros controladores
        Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario);
        Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario);
    }
}
