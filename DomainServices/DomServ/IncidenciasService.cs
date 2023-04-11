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
        private readonly IUsuariosConfiguradorQuery _user;

        public IncidenciasService(IIncidenciasRepo repo, IUsuariosConfiguradorQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<IncidenciasDto>> ObtenerIncidenciasPorOpcionAsync(string opcion, int idActivo, string criterio, int dias, string usuario)
        {
            var incids = new List<IncidenciasDto>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                switch(opcion) 
                {
                    case "IncidenciaAbiertaEnINSTALACION":
                        var abiertaInstalacion = await _repo.ObtenerIncidenciasAbiertasEnInstalacionAsync(idActivo);
                        incids = ConvierteListaIncidenciasInstalacionToDto(abiertaInstalacion);
                        break;
                    case "IncidenciaAbiertaEnESTRUCTURA":
                        var abiertaEstructura = await _repo.ObtenerIncidenciasAbiertasEnEstructuraAsync(idActivo);
                        incids = ConvierteListaIncidenciasEstructuraToDto(abiertaEstructura);
                        break;
                    case "IncidenciaSinAtenderPorVariosDiasEnESTRUCTURAS":
                        if (dias > 0) 
                        {
                            var diasAtras = -dias;
                            var noAtendidas = await _repo.ObtenerIncidenciasNoAtendidasPorDiasEnEstructurasAsync(diasAtras);
                            incids = ConvierteListaIncidenciasEstructuraToDto(noAtendidas);
                        }
                        break;
                    case "IncidenciaReportadaEnProgramaINSTALACION":
                        var idProgInsta = Int32.Parse(criterio);
                        var repProgInstalacion = await _repo.ObtenerIncidenciasReportadasEnProgramaEnInstalacionAsync(idProgInsta);
                        incids = ConvierteListaIncidenciasInstalacionToDto(repProgInstalacion);
                        break;
                    case "IncidenciaReportadaEnProgramaESTRUCTURA":
                        var idProgEstructura = Int32.Parse(criterio);
                        var repProgEstruct = await _repo.ObtenerIncidenciasReportadasEnProgramaEnEstructuraAsync(idProgEstructura);
                        incids = ConvierteListaIncidenciasEstructuraToDto(repProgEstruct);
                        break;
                }
            }

            return incids;
        }

        public async Task ActualizaIncidenciaAsync(IncidenciasDtoForUpdate i)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(i.Usuario);

            if (user != null)
            {
                switch (i.TipoIncidencia)
                {
                    case "INSTALACION":
                        await _repo.ActualizaReporteEnInstalacionAsync(i.IdReporte, i.DescripcionIncidencia, i.PrioridadIncidencia, i.IdClasificacionIncidencia, i.EstadoIncidencia);
                        break;
                    case "ESTRUCTURA":
                        await _repo.ActualizaReporteEnEstructuraAsync(i.IdReporte, i.DescripcionIncidencia, i.PrioridadIncidencia, i.IdClasificacionIncidencia, i.EstadoIncidencia);
                        break;
                }

                //TODO falta implementar la inserción en tarjetainformativa reporte
                if (i.IdReporte > 0 && i.IdTarjeta > 0)
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
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(i.Usuario);
            int idReporte=-1;

            if (user != null)
            {
                switch (i.TipoIncidencia)
                {
                    case "INSTALACION":
                        idReporte = await AgregaIncidenciaDeInstalacion(i);
                        break;
                    case "ESTRUCTURA":
                        idReporte = await AgregaIncidenciaDeEstructura(i);
                        break;
                }

                //TODO falta revisar si es correcta esta funcionalidad
                if (idReporte != -1 && i.IdTarjeta > 0)
                {
                    await _repo.AgregaTarjetaInformativaReporteAsync(i.IdTarjeta, idReporte, i.TipoIncidencia);
                }
            }
        }

        private async Task<int> AgregaIncidenciaDeInstalacion(IncidenciasDtoForCreate i) 
        {
            var abiertos = await _repo.ObtenerReportesAbiertosPorInstalacionAsync(i.Id, i.IdClasificacionIncidencia);
            int idReporte = -1;

            if (abiertos != null)
            {
                switch (abiertos.Count()) 
                {
                    case 0:
                        //No existe incidencia reportada
                        var rnvo = await _repo.AgregaReporteInstalacionAsync(i.Id, i.IdNota, i.DescripcionIncidencia, i.EstadoIncidencia, i.PrioridadIncidencia, i.IdClasificacionIncidencia);
                        idReporte = rnvo.IdReportePunto;
                        break;
                    default:
                        // Ya existe incidencia reportada
                        idReporte = abiertos[0].id_reporte;
                        var incidenciaActualizada = abiertos[0].incidencia + " / " + i.DescripcionIncidencia;

                        await _repo.ActualizaReporteEnInstalacionPorIncidenciaExistenteAsync(idReporte, incidenciaActualizada, i.PrioridadIncidencia);
                        break;
                }
            }

            return idReporte;
        }

        private async Task<int> AgregaIncidenciaDeEstructura(IncidenciasDtoForCreate i)
        {
            var abiertos = await _repo.ObtenerReportesAbiertosPorEstructuraAsync(i.Id, i.IdClasificacionIncidencia);
            int idReporte = -1;

            if (abiertos != null)
            {
                switch (abiertos.Count())
                {
                    case 0:
                        //No existe incidencia reportada
                        var rnvo = await _repo.AgregaReporteEstructuraAsync(i.Id, i.IdNota, i.DescripcionIncidencia, i.EstadoIncidencia, i.PrioridadIncidencia, i.IdClasificacionIncidencia);
                        idReporte = rnvo.IdReporte;
                        break;
                    default:
                        // Ya existe incidencia reportada
                        idReporte = abiertos[0].id_reporte;
                        var incidenciaActualizada = abiertos[0].incidencia + " / " + i.DescripcionIncidencia;

                        await _repo.ActualizaReporteEnEstructuraPorIncidenciaExistenteAsync(idReporte, incidenciaActualizada, i.PrioridadIncidencia);
                        break;
                }
            }

            return idReporte;
        }

        private IncidenciasDto ConvierteIncidenciaInstalacionToDto(IncidenciaInstalacionVista i) 
        {
            var incidencia = new IncidenciasDto() 
            {
                IdReporte = i.id_reportePunto,
                IdNota = i.id_nota,
                Incidencia = i.incidencia,
                EstadoIncidencia = i.estadoIncidencia,
                PrioridadIncidencia = i.prioridadIncidencia,
                IdClasificacionIncidencia = i.id_clasificacionIncidencia,
                UltimaActualizacion = i.ultimaActualizacion,
                Coordenadas = i.coordenadas,
                IdProcesoResponsable = i.id_ProcesoResponsable,
                IdGerenciaDivision = i.id_GerenciaDivision,
                DescripcionEstado = i.descripcionEstado,
                DescripcionNivel = i.descripcionnivel,
                TipoReporte = i.tiporeporte,

                Punto = i.id_punto,
                Ubicacion = i.ubicacion,
            };

            return incidencia;
        }
        private IncidenciasDto ConvierteIncidenciaEstructuraToDto(IncidenciaEstructuraVista i) 
        {
            var incidencia = new IncidenciasDto()
            {
                IdReporte = i.id_reporte,
                IdNota = i.id_nota,                
                Incidencia = i.incidencia,
                EstadoIncidencia = i.estadoIncidencia,
                PrioridadIncidencia = i.prioridadIncidencia,
                IdClasificacionIncidencia = i.id_clasificacionIncidencia,
                UltimaActualizacion = i.ultimaActualizacion,
                Coordenadas = i.coordenadas,
                IdProcesoResponsable = i.id_ProcesoResponsable,
                IdGerenciaDivision = i.id_GerenciaDivision,
                DescripcionEstado = i.descripcionEstado,
                DescripcionNivel = i.descripcionnivel,
                TipoReporte = i.tiporeporte,

                IdEstructura = i.id_estructura,
                Clave = i.clave,
                Nombre = i.nombre,
            };

            return incidencia;
        }

        private List<IncidenciasDto> ConvierteListaIncidenciasEstructuraToDto(List<IncidenciaEstructuraVista> incidencias) 
        {
            var lstIncidencias = new List<IncidenciasDto>();

            foreach (var item in incidencias)
            {
                lstIncidencias.Add(ConvierteIncidenciaEstructuraToDto(item));
            }

            return lstIncidencias;
        }
        private List<IncidenciasDto> ConvierteListaIncidenciasInstalacionToDto(List<IncidenciaInstalacionVista> incidencias) 
        {
            var lstIncidencias = new List<IncidenciasDto>();
            
            foreach (var item in incidencias)
            {
                lstIncidencias.Add(ConvierteIncidenciaInstalacionToDto(item));
            }

            return lstIncidencias;
        }
    }
}

