using Domain.Ports.Driven.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Ports.Driven;
using Domain.DTOs;
using Domain.Ports.Driving;
using Domain.Entities.Vistas;

namespace DomainServices.DomServ
{
    public class IncidenciasService : IIncidenciasService 
    {
        private readonly IIncidenciasRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public IncidenciasService(IIncidenciasRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<IncidenciaGeneralDto>> ObtenerIncidenciasPorOpcionAsync(string opcion, int idActivo, string criterio, string usuario)
        {
            var incids = new List<IncidenciaGeneralDto>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var datosComplementarios = "";

                if (opcion.Contains("-"))
                {
                    var dataOption = opcion.Split("-");
                    opcion = dataOption[0];
                    datosComplementarios = dataOption[1];
                }

                switch(opcion) 
                {
                    case "IncidenciaAbiertaEnINSTALACION":
                        var abiertaInstalacion = await _repo.ObtenerIncidenciasAbiertasEnInstalacionAsync(idActivo);
                        incids = ConvierteListaIncidenciasGeneralesToDto(abiertaInstalacion);
                        break;
                    case "IncidenciaAbiertaEnESTRUCTURA":
                        var abiertaEstructura = await _repo.ObtenerIncidenciasAbiertasEnEstructuraAsync(idActivo);
                        incids = ConvierteListaIncidenciasGeneralesToDto(abiertaEstructura);
                        break;
                    case "IncidenciaSinAtenderPorVariosDiasEnESTRUCTURAS":
                            var dias = Int32.Parse(criterio);
                            var diasAtras = -dias;
                            var noAtendidas = await _repo.ObtenerIncidenciasNoAtendidasPorDiasEnEstructurasAsync(diasAtras);
                            incids = ConvierteListaIncidenciasGeneralesToDto(noAtendidas);                        
                        break;
                    case "IncidenciaReportadaEnProgramaINSTALACION":
                        var idProgInsta = Int32.Parse(criterio);
                        var repProgInstalacion = await _repo.ObtenerIncidenciasReportadasEnProgramaEnInstalacionAsync(idProgInsta);
                        incids = ConvierteListaIncidenciasGeneralesToDto(repProgInstalacion);
                        break;
                    case "IncidenciaReportadaEnProgramaESTRUCTURA":
                        var idProgEstructura = Int32.Parse(criterio);
                        var repProgEstruct = await _repo.ObtenerIncidenciasReportadasEnProgramaEnEstructuraAsync(idProgEstructura);
                        incids = ConvierteListaIncidenciasGeneralesToDto(repProgEstruct);
                        break;
                    case "TodosPorBusquedaINSTALACION":
                        var allInstalacion = await _repo.ObtenerIncidenciasInstalacionPorUbicacionOrIncidenciaAsync(criterio);
                        incids = ConvierteListaIncidenciasGeneralesToDto(allInstalacion);
                        break;
                    case "TodosPorBusquedaESTRUCTURA":
                        var allEstructura = await _repo.ObtenerIncidenciasEstructuraPorUbicacionOrIncidenciaAsync(criterio);
                        incids = ConvierteListaIncidenciasGeneralesToDto(allEstructura);
                        break;
                    case "NoConcluidosPorBusquedaINSTALACION":
                        var noConc = await _repo.ObtenerIncidenciasNoConcluidasInstalacionAsync(criterio);
                        incids = ConvierteListaIncidenciasGeneralesToDto(noConc);
                        break;
                    case "NoConcluidosPorBusquedaESTRUCTURA":
                        var noConcEst = await _repo.ObtenerIncidenciasNoConcluidasEstructuraAsync(criterio);
                        incids = ConvierteListaIncidenciasGeneralesToDto(noConcEst);
                        break;
                    case "EnUnEstadoEspecificoPorBusquedaINSTALACION":
                        var comp = Int32.Parse(datosComplementarios);
                        var edoInsta = await _repo.ObtenerIncidenciasInstalacionEstadoEspecificoAsync(criterio, comp);
                        incids = ConvierteListaIncidenciasGeneralesToDto(edoInsta);
                        break;
                    case "EnUnEstadoEspecificoPorBusquedaESTRUCTURA":
                        var compEst = Int32.Parse(datosComplementarios);
                        var edoEstruct = await _repo.ObtenerIncidenciasEstructuraEstadoEspecificoAsync(criterio, compEst);
                        incids = ConvierteListaIncidenciasGeneralesToDto(edoEstruct);
                        break;
                }
            }

            return incids;
        }

        public async Task ActualizaIncidenciaAsync(IncidenciasDtoForCreate i)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(i.strUsuario);

            if (user != null)
            {
                switch (i.strTipoIncidencia)
                {
                    case "INSTALACION":
                        await _repo.ActualizaReporteEnInstalacionAsync(i.intIdReporte, i.strDescripcionIncidencia, i.intIdPrioridadIncidencia, i.intIdClasificacionIncidencia, i.intIdEstadoIncidencia);
                        break;
                    case "ESTRUCTURA":
                        await _repo.ActualizaReporteEnEstructuraAsync(i.intIdReporte, i.strDescripcionIncidencia, i.intIdPrioridadIncidencia, i.intIdClasificacionIncidencia, i.intIdEstadoIncidencia);
                        break;
                }

                //TODO falta implementar la inserción en tarjetainformativa reporte
                if (i.intIdReporte > 0 && i.intIdTarjeta > 0)
                {
                    /*                    strInstruccionSQL = "INSERT INTO tarjetainformativareporte (idtarjeta,idreporte,idtiporeporte)
                                                                    SELECT " & objIncidencia.intIdTarjeta & "
                                                                    ," & intIdReporte & "
                                                                    ,(SELECT idtiporeporte FROM tiporeporte WHERE descripcion = '" & objIncidencia.strTipoIncidencia & "') FROM(SELECT 1, 2, 3) AS tmp_name
                                                                    WHERE NOT EXISTS(
                                                                        SELECT idtarjeta FROM tarjetainformativareporte WHERE idtarjeta = " & objIncidencia.intIdTarjeta & "
                                                                            AND idreporte = " & intIdReporte & "
                                                                            AND idtiporeporte = (SELECT idtiporeporte FROM tiporeporte WHERE descripcion = '" & objIncidencia.strTipoIncidencia & "')
                                                                    ) LIMIT 1; "*/
                }
            }
        }

        public async Task AgregaIncidenciaAsync(IncidenciasDtoForCreate i) 
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(i.strUsuario);
            int idReporte=-1;

            if (user != null)
            {
                switch (i.strTipoIncidencia)
                {
                    case "INSTALACION":
                        idReporte = await AgregaIncidenciaDeInstalacion(i);
                        break;
                    case "ESTRUCTURA":
                        idReporte = await AgregaIncidenciaDeEstructura(i);
                        break;
                }

                //TODO falta revisar si es correcta esta funcionalidad
                if (idReporte != -1 && i.intIdTarjeta > 0)
                {
                    await _repo.AgregaTarjetaInformativaReporteAsync(i.intIdTarjeta, idReporte, i.strTipoIncidencia);
                }
            }
        }

        private async Task<int> AgregaIncidenciaDeInstalacion(IncidenciasDtoForCreate i) 
        {
            var abiertos = await _repo.ObtenerReportesAbiertosPorInstalacionAsync(i.intIdActivo, i.intIdClasificacionIncidencia);
            int idReporte = -1;

            if (abiertos != null)
            {
                switch (abiertos.Count()) 
                {
                    case 0:
                        //No existe incidencia reportada
                        var rnvo = await _repo.AgregaReporteInstalacionAsync(i.intIdActivo, i.intIdReporte, i.strDescripcionIncidencia, i.intIdEstadoIncidencia, i.intIdPrioridadIncidencia, i.intIdClasificacionIncidencia);
                        idReporte = rnvo.IdReportePunto;
                        break;
                    default:
                        // Ya existe incidencia reportada
                        idReporte = abiertos[0].id_reporte;
                        var incidenciaActualizada = abiertos[0].incidencia + " / " + i.strDescripcionIncidencia;

                        await _repo.ActualizaReporteEnInstalacionPorIncidenciaExistenteAsync(idReporte, incidenciaActualizada, i.intIdPrioridadIncidencia);
                        break;
                }
            }

            return idReporte;
        }

        private async Task<int> AgregaIncidenciaDeEstructura(IncidenciasDtoForCreate i)
        {
            var abiertos = await _repo.ObtenerReportesAbiertosPorEstructuraAsync(i.intIdActivo, i.intIdClasificacionIncidencia);
            int idReporte = -1;

            if (abiertos != null)
            {
                switch (abiertos.Count())
                {
                    case 0:
                        //No existe incidencia reportada
                        var rnvo = await _repo.AgregaReporteEstructuraAsync(i.intIdActivo, i.intIdReporte, i.strDescripcionIncidencia, i.intIdEstadoIncidencia, i.intIdPrioridadIncidencia, i.intIdClasificacionIncidencia);
                        idReporte = rnvo.IdReporte;
                        break;
                    default:
                        // Ya existe incidencia reportada
                        idReporte = abiertos[0].id_reporte;
                        var incidenciaActualizada = abiertos[0].incidencia + " / " + i.strDescripcionIncidencia;

                        await _repo.ActualizaReporteEnEstructuraPorIncidenciaExistenteAsync(idReporte, incidenciaActualizada, i.intIdPrioridadIncidencia);
                        break;
                }
            }

            return idReporte;
        }

        private IncidenciaGeneralDto ConvierteIncidenciaToIncidenciaGeneralDto(IncidenciaGeneralVista i)
        {
            var incidencia = new IncidenciaGeneralDto()
            {
                intIdReporte = i.id_reporte,
                intIdTarjeta = i.id_nota,
                strLinea = i.ubicacion,
                strEstructura = "",
                strCoordenadas = i.coordenadas,
                intIdProcesoResponsable = i.id_procesoresponsable,
                intIdGerenciaDivision = i.id_gerenciadivision,
                strDescripcionIncidencia = i.incidencia,
                intIdEstadoIncidencia = i.estadoincidencia,
                strEstadoIncidencia = i.descripcionestado,
                strUltimaActualizacion = i.ultimaactualizacion?.ToString("yyyy-MM-dd HH:mm:ss"),
                intIdPrioridadIncidencia = i.prioridadincidencia,
                intIdClasificacionIncidencia = i.id_clasificacionincidencia,
                strTipoIncidencia = i.tiporeporte,
                strDescripcionPrioridadIncidencia = i.descripcionnivel,
                strDescripcionClasificacionIncidencia = i.descripcion,
            };

            return incidencia;
        }

        private List<IncidenciaGeneralDto> ConvierteListaIncidenciasGeneralesToDto(List<IncidenciaGeneralVista> incidencias)
        {
            var lstIncidencias = new List<IncidenciaGeneralDto>();

            foreach (var item in incidencias)
            {
                lstIncidencias.Add(ConvierteIncidenciaToIncidenciaGeneralDto(item));
            }

            return lstIncidencias;
        }
    }
}

