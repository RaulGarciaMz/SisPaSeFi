using Domain.DTOs;
using Domain.Entities;
using Domain.Entities.Vistas;

namespace Domain.Ports.Driven
{
    public interface IUsuariosCommand
    {

        Task AgregaUsuarioDeDocumentoAsync(int idDocumento, int idUsuario);
        Task BorraUsuarioDeDocumentoAsync(int idDocumento, int idUsuario);

        Task ActualizaListasDeUsuariosDesbloquearAsync(List<string> desbloquear);
        Task ActualizaListasDeUsuariosBloquearAsync(List<string> bloquear);
        Task ActualizaListasDeUsuariosReiniciarClaveAsync(List<string> reiniciar);
        Task ActualizaListasDeUsuariosAsync(List<UsuarioDto> usuarios);
        
    }

    public interface IUsuariosComandanciaCommand
    {
        Task BorraListaDeUsuariosComandanciaAsync(List<UsuarioComandancia> usuarios);
        Task AgregaListaDeUsuariosComandanciaAsync(List<UsuarioComandancia> usuarios);
    }

    public interface IUsuariosComandanciaQuery
    {
        Task<List<UsuarioComandancia>> ObtenerUsuariosComandanciaPorIdUsuarioAndIdComandanciaAsync(int idUsuario, int idComandancia);
    }

    public interface IUsuariosRolCommand
    {
        Task AgregaListaDeUsuariosRolAsync(List<UsuarioRol> usuarios);
        Task BorraListaDeUsuariosRolAsync(List<UsuarioRol> usuarios);
    }

    public interface IUsuariosRolQuery
    {
        Task<List<UsuarioRol>> ObtenerUsuariosRolPorIdUsuarioAndIdRolAsync(int idUsuario, int idRol);
    }

    public interface IUsuarioGrupoCorreoElectronicoCommand
    {
        Task AgregaListaDeUsuariosGrupoCorreoElectronicoAsync(List<UsuarioGrupoCorreoElectronico> usuarios);
        Task BorraListaDeUsuariosGrupoCorreoElectronicoAsync(List<UsuarioGrupoCorreoElectronico> usuarios);
    }

    public interface IUsuarioGrupoCorreoElectronicoQuery
    {
        Task<List<UsuarioGrupoCorreoElectronico>> ObtenerUsuariosGrupoCorreoElectronicoPorIdUsuarioAndIdGrupoAsync(int idUsuario, int idGrupo);
    }

    public interface IUsuariosQuery
    {
        //Task<Usuario?> ObtenerUsuarioPorCriterioAsync(string criterio);
        Task<List<UsuarioVista>> ObtenerUsuariosPorCriterioAsync(string criterio);
        Task<List<UsuarioVista>> ObtenerUsuariosDeDocumentoAsync(int id);
        Task<List<UsuarioVista>> ObtenerUsuariosNoIncluidosEnDocumentoAsync(string criterio, int idDocumento);
        Task<Usuario?> ObtenerUsuarioPorNombreConDiferenteIdAsync(string usuario, int idUsuario);
    }

    public interface IUsuariosConfiguradorQuery
    {
        Task<Usuario?> ObtenerUsuarioConfiguradorPorIdAsync(int idUsuario);
        Task<Usuario?> ObtenerUsuarioConfiguradorPorNombreAsync(string usuario);
    }

    public interface IUsuariosRegistroQuery
    {
        Task<UsuarioRegistroVista?> ObtenerUsuarioParaRegistroAsync(string usuario);
        Task<int> IdentificaUsuarioLocalAsync(string usuario, string clave);
    }
}
