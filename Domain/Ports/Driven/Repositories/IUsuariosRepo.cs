namespace Domain.Ports.Driven.Repositories
{
    public interface IUsuariosRepo : IUsuariosCommand, IUsuariosComandanciaCommand, IUsuariosRolCommand, IUsuarioGrupoCorreoElectronicoCommand,  IUsuariosComandanciaQuery, IUsuariosRolQuery, IUsuarioGrupoCorreoElectronicoQuery, IUsuariosQuery, IUsuariosParaValidacionQuery, IUsuariosRegistroQuery
    {
    }
}
