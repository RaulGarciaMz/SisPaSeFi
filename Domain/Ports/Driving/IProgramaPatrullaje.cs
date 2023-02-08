using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IProgramaCommand
    {
        void Agrega(ProgramaPatrullaje pp);
        void Agrega2(ProgramaPatrullaje pp);
        void Update(ProgramaPatrullaje pp);
        void Update2(ProgramaPatrullaje pp);
        void Delete(int id);
        void Delete2(int id);
    }

    public interface IProgramaQuery
    {
        List<ProgramaPatrullaje> ObtenerPorFiltro(string tipo, int dia, int mes, int anio, int region, string clase, int opcion = 0, int periodo = 1);
    }
}
