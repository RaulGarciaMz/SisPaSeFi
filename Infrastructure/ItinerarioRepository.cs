using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SqlServerAdapter
{
    public class ItinerarioRepository : IItinerariosRepo
    {
        protected readonly ItinerarioContext _itinerarioContext;

         public ItinerarioRepository(ItinerarioContext itiContext)
        {
            _itinerarioContext = itiContext ?? throw new ArgumentNullException(nameof(itiContext));
        }


        public async Task<List<ItinerarioVista>> ObtenerItinerariosporRutaAsync(int idRuta) 
        {
            string sqlQuery = @"SELECT a.id_itinerario, a.id_ruta, a.id_punto, a.posicion,
                                       b.ubicacion, b.coordenadas, b.id_procesoresponsable, b.id_gerenciadivision
                                FROM ssf.itinerario a
                                JOIN ssf.puntospatrullaje b ON a.id_punto=b.id_punto
                                WHERE a.id_ruta= @pRuta   
                                ORDER BY a.posicion";

            object[] parametros = new object[]
            {
                new SqlParameter("@pRuta", idRuta),
            };

            return await _itinerarioContext.ItinerariosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<Itinerario>> ObtenerItinerariosporRutaAndPosicionAsync(int idRuta, int posicion)
        {
            return await _itinerarioContext.Itinerarios.Where(x => x.IdRuta == idRuta && x.Posicion == posicion).ToListAsync();
        }

        public async Task<List<Itinerario>> ObtenerItinerariosporRutaAndPosicionAndPuntoAsync(int idRuta, int posicion, int idPunto)
        {
            return await _itinerarioContext.Itinerarios.Where(x => x.IdRuta == idRuta && x.Posicion == posicion && x.IdPunto == idPunto).ToListAsync();
        }

        public async Task AgregaItinerarioAsync(int idRuta, int idPunto, int posicion)
        {
            var itinerario = new Itinerario() 
            { 
                IdRuta = idRuta,
                IdPunto = idPunto,
                Posicion = posicion,
                //Campos no nulos
                UltimaActualizacion = DateTime.UtcNow
            };

            _itinerarioContext.Itinerarios.Add(itinerario);
            await _itinerarioContext.SaveChangesAsync();
        }

        public async Task ActualizaItinerarioAsync(int idItinerario, int idPunto, int posicion)
        {
            var itinerario = await _itinerarioContext.Itinerarios.Where(x => x.IdItinerario == idItinerario).SingleOrDefaultAsync();

            if (itinerario != null)
            {
                itinerario.Posicion = posicion;
                itinerario.IdPunto = idPunto;

                _itinerarioContext.Itinerarios.Update(itinerario);
                await _itinerarioContext.SaveChangesAsync();
            }
        }

        public async Task BorraItinerarioAsync(int idItinerario)
        {
            var itinerario = await _itinerarioContext.Itinerarios.Where(x => x.IdItinerario == idItinerario).SingleOrDefaultAsync();

            if (itinerario != null)
            {
                _itinerarioContext.Itinerarios.Remove(itinerario);
                await _itinerarioContext.SaveChangesAsync();
            }
        }
    }
}


