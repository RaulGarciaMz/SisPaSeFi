using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter
{
    public class UsoVehiculoRepository : IUsoVehiculoRepo
    {
        protected readonly UsoVehiculoContext _usoVehiculoContext;
        public UsoVehiculoRepository(UsoVehiculoContext usovehiculoContext)
        {
            _usoVehiculoContext = usovehiculoContext ?? throw new ArgumentNullException(nameof(usovehiculoContext));
        }

        public async Task<List<UsoVehiculoVista>> ObtenerUsoVehiculosPorProgramaAsync(int idPrograma)
        {
            string sqlQuery = @"select a.id_usovehiculo, a.id_programa, a.id_vehiculo, a.id_usuariovehiculo, a.kminicio, 
                                       a.kmfin, a.consumocombustible, a.estadovehiculo, 
                                       COALESCE(b.numeroeconomico,'') numeroeconomico, COALESCE(b.matricula, '') matricula
                                FROM ssf.usovehiculo a
                                JOIN ssf.vehiculos b ON  a.id_vehiculo=b.id_vehiculo
                                WHERE a.id_programa= @pPrograma";

            object[] parametros = new object[]
            {
                new SqlParameter("@pPrograma", idPrograma)
            };

            return await _usoVehiculoContext.UsosVehiculosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task AgregaAsync(int idPrograma, int idVehiculo, int idUsuarioVehiculo, int kmInicio, int kmFin, int consumo, string? edoVehiculo)
        {
           var uso = new UsoVehiculo()
           {
               IdPrograma = idPrograma,
               IdVehiculo = idVehiculo,
               IdUsuarioVehiculo = idUsuarioVehiculo,
               KmInicio = kmInicio,
               KmFin = kmFin,
               ConsumoCombustible = consumo,
               EstadoVehiculo = edoVehiculo
           };

           _usoVehiculoContext.UsosVehiculos.Add(uso);
           await _usoVehiculoContext.SaveChangesAsync();
        }

        public async Task ActualizaAsync(int idPrograma, int idVehiculo, int idUsuarioVehiculo, int kmInicio, int kmFin, int consumo, string? edoVehiculo)
        {
            var uso = await _usoVehiculoContext.UsosVehiculos.Where(x => x.IdPrograma == idPrograma && x.IdVehiculo == idVehiculo).SingleOrDefaultAsync();

            if (uso != null)
            {
                uso.IdUsuarioVehiculo = idUsuarioVehiculo;
                uso.KmInicio = kmInicio;
                uso.KmFin = kmFin;
                uso.ConsumoCombustible = consumo;
                uso.EstadoVehiculo = edoVehiculo;

                _usoVehiculoContext.UsosVehiculos.Update(uso);
                await _usoVehiculoContext.SaveChangesAsync();
            }
        }

        public async Task BorraAsync(int idPrograma, int idVehiculo)
        {
            var uso = await _usoVehiculoContext.UsosVehiculos.Where(x => x.IdPrograma == idPrograma && x.IdVehiculo == idVehiculo).SingleOrDefaultAsync();

            if (uso != null)
            {
                _usoVehiculoContext.UsosVehiculos.Remove(uso);
                await _usoVehiculoContext.SaveChangesAsync();
            }
        }
    }
}

