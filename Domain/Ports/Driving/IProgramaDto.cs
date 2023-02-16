using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IProgramaDtoCommand
    {
    }
    public interface IProgramaDtoQuery
    {

        List<PatrullajeDto> ObtenerPorFiltro(string tipo, int region, string clase,int anio, int mes, int dia, int opcion=0, int periodo=1);
    }
}
