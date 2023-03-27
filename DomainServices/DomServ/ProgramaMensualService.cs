using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driven;
using Domain.Ports.Driving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

namespace DomainServices.DomServ
{
    public class ProgramaMensualService : IProgramaMensualService
    {
        private readonly IProgramaMensualRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public ProgramaMensualService(IProgramaMensualRepo repo, IUsuariosConfiguradorQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<ProgramaPatrullajeMensualDto> ObtenerProgramaMensualAsync(int anio, int mes, string region, string tipo, string usuario)
        {
            var prog = new ProgramaPatrullajeMensualDto();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var regiones = await _repo.ObtenerRegionesMilitaresProgramaMensualAsync(anio, mes, region, tipo);

                if (regiones != null && regiones.Count > 0) 
                {
                    var reg = regiones[0];

                    prog.Municipio = reg.municipio;
                    prog.Estado = reg.estado;
                    prog.NombreResponsable = reg.nombre;
                    prog.ApellidoResponsable1 = reg.apellido1;
                    prog.ApellidoResponsable2 = reg.apellido2;
                    prog.RegionesMilitares = reg.regionesmilitares;

                    var miProg = await _repo.ObtenerProgramaMensualAsync(anio, mes, region, tipo);

                    var rutas = new List<RutasProgramaPatrullajeMensualDto>();

                    foreach (var p in miProg)
                    {
                        var v = new RutasProgramaPatrullajeMensualDto() { 
                        IdRuta = p.id_ruta,
                        Clave = p.clave,
                        RegionMilitar = p.regionmilitarsdn,
                        RegionSSF = p.regionssf,
                        ZonaMilitar = p.zonamilitarsdn,
                        ItinerarioRuta = p.itinerarioruta,
                        Fechas = p.fechas,
                        ObservacionesRuta = p.observacionesruta
                        };

                        var recorridos = new List<RecorridoRutaDto>();
                        if (p.itinerariorutapatrullaje.Length > 0)
                        { 
                            var ptosItinerario = p.itinerariorutapatrullaje.Split("¦");
                            foreach (var itin in ptosItinerario)
                            {
                                var pto = itin.Replace("[!:!]", "¦");
                                var datoItinerario =  pto.Split("¦");
                                var recorrido = new RecorridoRutaDto() {
                                    Posicion = Int32.Parse(datoItinerario[0]),
                                    Ubicacion = datoItinerario[1],
                                    Coordenadas = datoItinerario[2]
                                };

                                recorridos.Add(recorrido);
                            }

                            v.RecorridoRuta = recorridos;
                        }

                        rutas.Add(v);
                    }
                }               
            }

            return prog;

        }
    }
}
