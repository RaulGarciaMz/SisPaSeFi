﻿using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IIncidenciasDtoComand
    {
        Task AgregaIncidenciaAsync(IncidenciasDtoForCreate i);
        Task ActualizaIncidenciaAsync(IncidenciasDtoForCreate i);
    }

    public interface IIncidenciasDtoQuery
    {
        Task<List<IncidenciasDto>> ObtenerIncidenciasPorOpcionAsync(string opcion, int idActivo,  string criterio, int dias, string usuario);
    }
}
