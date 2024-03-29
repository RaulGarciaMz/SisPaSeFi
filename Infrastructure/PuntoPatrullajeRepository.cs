﻿using Domain.Entities;
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
using NetTopologySuite.Geometries;
using NetTopologySuite;

namespace SqlServerAdapter
{
    public class PuntoPatrullajeRepository : IPuntoPatrullajeRepo
    {
        protected readonly PuntoPatrullajeContext _patrullajeContext;
        private readonly GeometryFactory _geometryFactory;

        public PuntoPatrullajeRepository(PuntoPatrullajeContext patrullajeContext) 
        {
            _patrullajeContext = patrullajeContext;
            _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        }

        /// <summary>
        /// Método <c>ObtenerPorEstado</c> Obtiene puntos de patrullaje pertenecientes al estado indicado
        /// </summary>
        public List<PuntoPatrullaje> ObtenerPorEstado(int id_estado)
        {
            return _patrullajeContext.PuntosPatrullaje
                .Include(m => m.IdMunicipioNavigation.IdEstadoNavigation)
                .Where(c => c.IdMunicipioNavigation.IdEstado == id_estado)
                .ToList();
        }

        /// <summary>
        /// Método <c>ObtenerPorUbicacion</c> Obtiene puntos de patrullaje cuya ubicación (nombre) coincida con el parámetro
        /// </summary>
        public List<PuntoPatrullaje> ObtenerPorUbicacion(string ubicacion)
        {
            return _patrullajeContext.PuntosPatrullaje
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
            var pp = _patrullajeContext.PuntosPatrullaje
                .Where(x => x.Id == id && x.Bloqueado == false)
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

        public bool CoordenadasEnUso(double x, double y) 
        {
            var c = _geometryFactory.CreatePoint(new Coordinate(x, y));

            var cuantos =_patrullajeContext.PuntosPatrullaje.Where(x => x.CoordenadasSrid.Coordinate.Equals(c.Coordinate)).Count();

            if (cuantos > 0) 
            {
                return true;
            }

            return false;
        }

        public bool CoordenadasEnUsoEnPuntoDiferente(double x, double y, int idPunto)
        {
            var c = _geometryFactory.CreatePoint(new Coordinate(x, y));

            var cuantos = _patrullajeContext.PuntosPatrullaje.Where(x => x.CoordenadasSrid.Coordinate.Equals(c.Coordinate) && x.Id != idPunto).Count();

            if (cuantos > 0)
            {
                return true;
            }

            return false;
        }

    }











}