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
            _patrullajeContext = patrullajeContext ?? throw new ArgumentNullException(nameof(patrullajeContext));
        }

        /// <summary>
        /// Método <c>ObtenerPorEstado</c> Obtiene puntos de patrullaje pertenecientes al estado indicado
        /// </summary>
        public async Task<List<PuntoPatrullaje>> ObtenerPorEstadoAsync(int id_estado)
        {
            return await _patrullajeContext.puntospatrullaje
                .Include(m => m.IdMunicipioNavigation.IdEstadoNavigation)
                .Where(c => c.IdMunicipioNavigation.IdEstado == id_estado)
                .ToListAsync();
        }

        /// <summary>
        /// Método <c>ObtenerPorUbicacion</c> Obtiene puntos de patrullaje cuya ubicación (nombre) coincida con el parámetro
        /// </summary>
        public async Task<List<PuntoPatrullaje>> ObtenerPorUbicacionAsync(string ubicacion)
        {
            return await _patrullajeContext.puntospatrullaje
                .Include(m => m.IdMunicipioNavigation.IdEstadoNavigation)
                .Where(e => e.Ubicacion == ubicacion)
                .ToListAsync();
        }

        /// <summary>
        /// Método <c>Agrega</c> implementa la interface para registrar puntos de patrullaje
        /// </summary>
        public async Task Agrega(PuntoPatrullaje pp)
        {
            _patrullajeContext.Add(pp);
            await _patrullajeContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método <c>Update</c> implementa la interface para actualizar puntos de patrullaje.
        /// </summary>
        public async Task Update(PuntoPatrullaje pp)
        {
            _patrullajeContext.Update(pp);
            await _patrullajeContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método <c>Delete</c> implementa la interface para eliminar el punto de patrullaje indicado, siempre y cuando no esté bloqueado
        /// </summary>
        public async Task Delete(int id)
        {
            var pp = _patrullajeContext.puntospatrullaje
                .Where(x => x.IdPunto == id && x.Bloqueado == 0)
                .FirstOrDefault();

            if (pp != null)
            {
                _patrullajeContext.Remove(pp);
                await _patrullajeContext.SaveChangesAsync();
            }            
        }

        public async Task<int> ObtenerItinerariosPorPuntoAsync(int id)
        {
            return await _patrullajeContext.Itinerarios.Where(x => x.IdPunto == id).CountAsync();
        }

        public async Task<int> ObtenerIdUsuarioConfiguradorAsync(string usuario_nom)
        {
            var user = await _patrullajeContext.Usuarios.Where(x => x.UsuarioNom == usuario_nom && x.Configurador == 1).Select(x => x.IdUsuario).ToListAsync();

            if (user.Count == 0)
            {
                return -1;
            }
            else
            {
                return user[0];
            }
        }

        public async Task<bool> SaveChangesAsync() 
        {
            return (await _patrullajeContext.SaveChangesAsync() >= 0 );
        }
    }











}