using Domain.DTOs;
using Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IUsuariosDtoCommand
    {
/*        Task BloqueaUsuarioAsync(string usuario);
        Task DesbloqueaUsuarioAsync(string usuario);
        Task ReiniciaClaveUsuarioAsync(string usuario);*/
        Task ActualizaUsuariosPorOpcionAsync(string opcion, string usuario, List<UsuarioDto> users);
        Task AgregaPorOpcionAsync(string opcion, string dato, string usuario);
        Task BorraPorOpcionAsync(string opcion, string dato, string usuario);
    }

    public interface IUsuariosDtoQuery
    {
        Task<List<UsuarioDto>> ObtenerUsuarioPorOpcionAsync(string opcion, string criterio, string usuario);
        //Task<UsuarioDto?> ObtenerUsuarioPorCriterioAsync(string criterio);
        //Task<List<UsuarioDto>> ObtenerUsuariosPorCriterioAsync(string criterio);
        // Task<List<UsuarioDto>> ObtenerUsuarioPorOpcionAsync(string opcion, string criterio, string usuario);

        //Métodos comunes para ser usados por otros controladores
        Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario);
        Task<UsuarioDto?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario);
    }
}
