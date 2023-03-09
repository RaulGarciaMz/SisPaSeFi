using Domain.DTOs.catalogos;
using Domain.Entities;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.DomServ
{
    public class CatalogoConsultasService : ICatalogosConsultaService
    {
        private readonly ICatalogosConsultaRepo _repo;

        public CatalogoConsultasService(ICatalogosConsultaRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<CatalogoGenerico>> ObtenerClasificacionesIncidenciaAsync()
        {
           var cat = new List<CatalogoGenerico>();
           var clas = await _repo.ObtenerClasificacionesIncidenciaAsync();

            foreach (var c in clas) 
            {
                var row = new CatalogoGenerico()
                { 
                    Id = c.IdClasificacionIncidencia,
                    Descripcion = c.Descripcion
                };

                cat.Add(row);
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerComandanciaPorIdUsuarioAsync(int idUsuario)
        {
            var cat = new List<CatalogoGenerico>();
            var clas = await _repo.ObtenerComandanciaPorIdUsuarioAsync(idUsuario);

            foreach (var c in clas)
            {
                var row = new CatalogoGenerico()
                {
                    Id = c.IdComandancia,
                    Descripcion = c.Numero.ToString()
                };

                cat.Add(row);
            }

            return cat;
        }

        public async  Task<List<CatalogoGenerico>> ObtenerConceptosAfectacionAsync()
        {
            var cat = new List<CatalogoGenerico>();
            var clas = await _repo.ObtenerConceptosAfectacionAsync();

            foreach (var c in clas)
            {
                var desc = c.Descripcion + "(" + c.PrecioUnitario.ToString() + "/" + c.Unidades + ")";
                var row = new CatalogoGenerico()
                {
                    Id = c.IdConceptoAfectacion,
                    Descripcion = desc
                };
                
                cat.Add(row);
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerNivelesAsync()
        {
            var cat = new List<CatalogoGenerico>();
            var clas = await _repo.ObtenerNivelesAsync();

            foreach (var c in clas)
            {
                var row = new CatalogoGenerico()
                {
                    Id = c.IdNivel,
                    Descripcion = c.DescripcionNivel
                };

                cat.Add(row);
            }

            return cat;
        }

        public async Task<List<int>> ObtenerRegionesMilitaresEnRutasAsync()
        {
            var clas = await _repo.ObtenerRegionesMilitaresEnRutanAsync();

            return clas.OrderBy(x => x).ToList();
        }

        public async Task<List<CatalogoGenerico>> ObtenerTiposPatrullajeAsync()
        {
            var cat = new List<CatalogoGenerico>();
            var clas = await _repo.ObtenerTiposPatrullajeAsync();

            foreach (var c in clas)
            {
                var row = new CatalogoGenerico()
                {
                    Id = c.IdTipoPatrullaje,
                    Descripcion = c.Descripcion
                };

                cat.Add(row);
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerTiposVehiculoAsync()
        {
            var cat = new List<CatalogoGenerico>();
            var clas = await _repo.ObtenerTiposVehiculoAsync();

            foreach (var c in clas)
            {
                var row = new CatalogoGenerico()
                {
                    Id = c.IdTipoVehiculo,
                    Descripcion = c.DescripciontipoVehiculo
                };

                cat.Add(row);
            }

            return cat;
        }
    }
}
