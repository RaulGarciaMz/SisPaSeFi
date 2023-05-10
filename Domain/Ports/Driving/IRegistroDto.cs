using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IRegistroDtoQuery
    {
        Task<UsuarioRegistradoDto> ObtenerUsuarioRegistradoAsync(UsuarioDtoForGet u, string pathLdap);
    }

    public interface IRegistroDtoCommand
    {
        Task<string> ActualizaRegistroPorOpcion(string opcion, UsuarioForPostDto user);
    }

    
}
