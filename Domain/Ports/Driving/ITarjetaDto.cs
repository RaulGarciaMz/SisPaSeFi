using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface ITarjetaDtoCommand
    {
        void Agrega(TarjetaDto tarjeta, string usuario);
        void Update(TarjetaDto tarjeta, string usuario);
    }

    public interface ITarjetaDtoQuery
    {
        List<TarjetaDto> ObtenerPorAnioMes(string tipo, string region, int anio, int mes, string usuario);
    }
}
