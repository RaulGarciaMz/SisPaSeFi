using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Ports.Driving;
using SqlServerAdapter;
using SqlServerAdapter.Data;

namespace SqlAdapter.Tests
{
    public class ProgramaRepositoryTests
    {
        [Fact]
        public void ObtenerProgramas_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());

            var x = pc.ObtenerProgramas("TERRESTRE",1);

            if (x.Count == 0)
            {
                   
            }            
        }

        [Fact]
        public void ObtenerPropuestasExtraordinariasPorAnioMesDia_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());

            var x = pc.ObtenerPropuestasExtraordinariasPorAnioMesDia("TERRESTRE", 1, 2020, 06, 15);

            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasPendientesPorAutorizarPorFiltro_TerrestreProgramado_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());

            var x = pc.ObtenerPropuestasPendientesPorAutorizarPorFiltro("TERRESTRE", 1, 2020, 06, "PROGRAMADO");
            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasPendientesPorAutorizarPorFiltro_TerrestreExtraordinario_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());

            var x = pc.ObtenerPropuestasPendientesPorAutorizarPorFiltro("TERRESTRE", 1, 2020, 06, "EXTRAORDINARIO");
            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasExtraordinariasPorFiltro_TerrestreExtraordinario_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerPropuestasExtraordinariasPorFiltro("TERRESTRE", 1, 2020, 06, "EXTRAORDINARIO");
            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasPorFiltroEstado_TerrestreExtraordinarioPendiente_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerPropuestasPorFiltroEstado("TERRESTRE", 5, 2022, 11, "EXTRAORDINARIO", "Pendiente de autorizacion por la SSF");
            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasPorFiltroEstado_TerrestreExtraordinarioAutorizada_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerPropuestasPorFiltroEstado("TERRESTRE", 5, 2022, 11, "EXTRAORDINARIO", "Autorizada");
            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasPorFiltroEstado_TerrestreExtraordinarioRechazada_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerPropuestasPorFiltroEstado("TERRESTRE", 4, 2022, 09, "EXTRAORDINARIO", "Rechazada");
            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasPorFiltroEstado_TerrestreExtraordinarioAprobada_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerPropuestasPorFiltroEstado("TERRESTRE", 4, 2022, 09, "EXTRAORDINARIO", "Aprobada por comandancia regional");
            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasExtraordinariasPorFiltroEstado_TerrestreExtraordinarioPendiente_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerPropuestasExtraordinariasPorFiltroEstado("TERRESTRE", 5, 2022, 11, "EXTRAORDINARIO", "Pendiente de autorizacion por la SSF");

            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasExtraordinariasPorFiltroEstado_TerrestreExtraordinarioAutorizada_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerPropuestasExtraordinariasPorFiltroEstado("TERRESTRE", 4, 2022, 09, "EXTRAORDINARIO", "Autorizada");

            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasExtraordinariasPorFiltroEstado_TerrestreExtraordinarioRechazada_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerPropuestasExtraordinariasPorFiltroEstado("TERRESTRE", 4, 2022, 09, "EXTRAORDINARIO", "Rechazada");

            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerPropuestasExtraordinariasPorFiltroEstado_TerrestreExtraordinarioAprobada_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerPropuestasExtraordinariasPorFiltroEstado("TERRESTRE", 4, 2022, 09, "EXTRAORDINARIO", "Aprobada por comandancia regional");

            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerProgramasEnProgresoPorDia_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasEnProgresoPorDia("TERRESTRE", 1, 2022, 09, 15);

            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerProgramasEnProgresoPorMes_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasEnProgresoPorMes("TERRESTRE", 1, 2017, 06);

            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerProgramasEnProgreso_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasEnProgreso("TERRESTRE", 1);
            
            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerProgramasConcluidosPorDia_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasConcluidosPorDia("TERRESTRE", 1, 2020,06,15);

            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerProgramasConcluidosPorMes_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasConcluidosPorMes("TERRESTRE", 1, 2020, 06);
            
            if (x.Count == 0)
            {

            }
        }

        [Fact]
        public void ObtenerProgramasConcluidos_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasConcluidos("TERRESTRE", 1);

            if (x.Count == 0)
            {
            }
        }
        
        [Fact]
        public void ObtenerProgramasCanceladosPorDia_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasCanceladosPorDia("TERRESTRE", 1,2020, 06,10);

            if (x.Count == 0)
            {
            }
        }

        [Fact]
        public void ObtenerProgramasCanceladosPorMes_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasCanceladosPorMes("TERRESTRE", 1, 2020, 06);

            if (x.Count == 0)
            {
            }
        }

        [Fact]
        public void ObtenerProgramasCancelados_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasCancelados("TERRESTRE", 1);

            if (x.Count == 0)
            {
            }
        }

        [Fact]
        public void ObtenerProgramasPorDia_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasPorDia("TERRESTRE", 1,2020,06,15);
            
            if (x.Count == 0)
            {
            }
        }

        [Fact]
        public void ObtenerProgramasPorMes_Terrestre_ReturnsOk()
        {
            var pc = new ProgramaPatrullajeRepository(new ProgramaContext());
            var x = pc.ObtenerProgramasPorMes("TERRESTRE", 1, 2020, 06);

            if (x.Count == 0)
            {
            }
        }

    }

}
