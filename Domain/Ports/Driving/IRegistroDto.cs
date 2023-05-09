using Domain.DTOs;

namespace Domain.Ports.Driving
{
    public interface IRegistroDtoQuery
    {
        Task<UsuarioRegistradoDto> ObtenerUsuarioRegistradoAsync(UsuarioDtoForGet u, string pathLdap);
    }
}
