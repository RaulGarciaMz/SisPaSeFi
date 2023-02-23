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
        /// Método <c>ObtenerPorEstado</c> Obtiene puntos de patrullaje pertenecientes al estado indicado
        /// </summary>
        public List<PuntoPatrullaje> ObtenerPorEstado(int id_estado)
        {
            return _patrullajeContext.puntospatrullaje
                .Include(m => m.IdMunicipioNavigation.IdEstadoNavigation)
                .Where(c => c.IdMunicipioNavigation.IdEstado == id_estado)
                .ToList();
        }

        /// <summary>
        /// Método <c>ObtenerPorUbicacion</c> Obtiene puntos de patrullaje cuya ubicación (nombre) coincida con el parámetro
        /// </summary>
        public List<PuntoPatrullaje> ObtenerPorUbicacion(string ubicacion)
        {
            return _patrullajeContext.puntospatrullaje
                .Include(m => m.IdMunicipioNavigation.IdEstadoNavigation)
                .Where(e => e.Ubicacion == ubicacion)
                .ToList();
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
        /// Método <c>Delete</c> implementa la interface para eliminar el punto de patrullaje indicado, siempre y cuando no esté bloqueado
        /// </summary>
        public void Delete(int id)
        {
            var pp = _patrullajeContext.puntospatrullaje
                .Where(x => x.IdPunto == id && x.Bloqueado == 0)
                .FirstOrDefault();

            if (pp != null)
            {
                _patrullajeContext.Remove(pp);
                _patrullajeContext.SaveChanges();
            }            
        }

        public int ObtenerItinerariosPorPunto(int id)
        {
            return _patrullajeContext.Itinerarios.Where(x => x.IdPunto == id).Count();
        }

        public int ObtenerIdUsuarioConfigurador(string usuario_nom)
        {
            var user = _patrullajeContext.Usuarios.Where(x => x.UsuarioNom == usuario_nom && x.Configurador == 1).Select(x => x.IdUsuario).ToList();

            if (user.Count == 0)
            {
                return -1;
            }
            else
            {
                return user[0];
            }
        }
    }











}