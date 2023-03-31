﻿using Domain.DTOs.catalogos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface ICatalogoConsultasDto
    {
        Task<List<CatalogoGenerico>> ObtenerComandanciaPorIdUsuarioAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerTiposPatrullajeAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerTiposVehiculoAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerClasificacionesIncidenciaAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerNivelesAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerConceptosAfectacionAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerRegionesMilitaresEnRutasConDescVaciaAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerResultadosPatrullajeAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerEstadosPaisAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerMunicipiosPorEstadoAsync(int idEstado, string usuario);
        Task<List<CatalogoGenerico>> ObtenerProcesosResponsablesAsync(string usuario);
        Task<List<CatalogoGenerico>> ObtenerGerenciaDivisionAsync(int idProceso, string usuario);
        Task<List<CatalogoGenerico>> ObtenerTiposDocumentosAsync(string usuario);        
        Task<List<CatalogoGenerico>> ObtenerCatalogoPorOpcionAsync(string opcion, string usuario);
    }
}
