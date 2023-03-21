using Domain.DTOs;
using Domain.Entities.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IUsuariosPatrullajeDtoCommand
    {
        Task Agregar(UsuarioPatrullajeDto usuario);
        Task Borrar(UsuarioPatrullajeDto usuario);
    }

    public interface IUsuariosPatrullajeDtoQuery
    {
        Task<List<UsuarioPatrullajeVista>> ObtenerIncidenciasPorOpcionAsync(string opcion, int idPrograma, int region, string usuario);
    }
}
