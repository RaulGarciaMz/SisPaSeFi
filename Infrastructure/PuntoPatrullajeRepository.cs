using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using Domain.Ports.Driven.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing;
using Domain.Enums;

namespace SqlServerAdapter
{
    public class PuntoPatrullajeRepository : IPuntoPatrullajeRepo
    {
        protected readonly PatrullajeContext _patrullajeContext;

        public PuntoPatrullajeRepository(PatrullajeContext patrullajeContext) 
        {
            _patrullajeContext = patrullajeContext;
        }

        /// <summary>
        /// Método <c>Obtener</c> Obtiene todos los puntos de patrullaje
        /// </summary>
        private IEnumerable<PuntoPatrullaje> Obtener()
        {
            return _patrullajeContext.puntospatrullaje.ToList();
        }

        /// <summary>
        /// Método <c>ObtenerPorEstado</c> Obtiene puntos de patrullaje pertenecientes al estado indicado
        /// </summary>
        private IEnumerable<PuntoPatrullaje> ObtenerPorEstado(int id_estado)
        {

            return _patrullajeContext.puntospatrullaje
                .Where(c => c.Municipio.id_estado == id_estado)
                .ToList();
        }

        /// <summary>
        /// Método <c>ObtenerPorUbicacion</c> Obtiene puntos de patrullaje cuya ubicación (nombre) coincida con el parámetro
        /// </summary>
        private IEnumerable<PuntoPatrullaje> ObtenerPorUbicacion(string ubicacion)
        {
            return _patrullajeContext.puntospatrullaje
                .Where(e => e.ubicacion == ubicacion)
                .ToList();
        }

        /// <summary>
        /// Método <c>ObtenerPorOpcion</c> implementa la interface para obtener puntos de patrullaje por estado o por ubicación.
        /// </summary>
        public IEnumerable<PuntoPatrullaje> ObtenerPorOpcion(FiltroPunto opcion, string valor)
        {
            List<PuntoPatrullaje> l = null;

            switch (opcion)
            {
                case FiltroPunto.Estado:
                    var b = int.TryParse(valor, out int j);
                    if (!b)
                    {
                        //Mandar error
                    }

                    l = ObtenerPorEstado(j).ToList();
                    break;

                case FiltroPunto.Ubicacion:
                    l = ObtenerPorUbicacion(valor).ToList();
                    break;
            }

            return l;
        }

        /// <summary>
        /// Método <c>Agrega</c> implementa la interface para registrar puntos de patrullaje
        /// </summary>
        public void Agrega(PuntoPatrullaje pp)
        {
            _patrullajeContext.Add(pp);
            _patrullajeContext.SaveChanges();
        }

        /// <summary>
        /// Método <c>Update</c> implementa la interface para actualizar puntos de patrullaje.
        /// </summary>
        public void Update(PuntoPatrullaje pp)
        {
            _patrullajeContext.Update(pp);
            _patrullajeContext.SaveChanges();
        }

        /// <summary>
        /// Método <c>Delete</c> implementa la interface para eliminar el punto de patrullaje indicado, siempre y cuando no esté bloqueado y no esté en algún itinerario
        /// </summary>
        public void Delete(int id)
        {
            int numItinerarios = _patrullajeContext.Itinerarios
                .Where(x => x.id_punto == id)
                .Count();

            if (numItinerarios == 0)
            {
                var pp = _patrullajeContext.puntospatrullaje
                    .Where(x => x.id_punto == id && x.bloqueado == 0)
                    .FirstOrDefault();

                if (pp != null)
                {
                    _patrullajeContext.Remove(pp);
                    _patrullajeContext.SaveChanges();
                }
            }
        }
    }











}