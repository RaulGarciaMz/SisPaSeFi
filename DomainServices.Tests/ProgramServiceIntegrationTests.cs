using Domain.Enums;
using Domain.Ports.Driving;
using DomainServices.DomServ;
using SqlServerAdapter;
using SqlServerAdapter.Data;

namespace DomainServices.Tests
{
    public class ProgramServiceIntegrationTests
    {
        [Fact]
        public void ObtenerExtraordinariosyProgramados_Ordinario_ReturnsOk()
        {     
            //Caso 0 - Ordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "PROGRAMADO", 2020,12);

            if (r.Count== 0) { }
        }

        [Fact]
        public void ObtenerExtraordinariosyProgramados_Extraordinario_ReturnsOk()
        {
            //Caso 0 - Extraordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2020, 12);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerEnProgresoPorDia_ReturnsOk()
        {
            //Caso 1 - Patrullajes en prograso por día
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajesEnProgreso, PeriodoOpcion.UnDia);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerEnProgresoPorMes_ReturnsOk()
        {
            //Caso 1 - Patrullajes en prograso por mes
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajesEnProgreso, PeriodoOpcion.UnMes);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerEnProgreso_ReturnsOk()
        {
            //Caso 1 - Patrullajes en prograso todos
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajesEnProgreso, PeriodoOpcion.Todos);

            if (r.Count == 0) { }
        }

       
        [Fact]
        public void ObtenerConcluidosPorDia_ReturnsOk()
        {
            //Caso 2 - Patrullajes concluidos por día
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajesConcluidos, PeriodoOpcion.UnDia);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerConcluidosPorMes_ReturnsOk()
        {
            //Caso 2 - Patrullajes concluidos por mes
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajesConcluidos, PeriodoOpcion.UnMes);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerConcluidos_ReturnsOk()
        {
            //Caso 2 - Patrullajes concluidos todos
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajesConcluidos, PeriodoOpcion.Todos);

            if (r.Count == 0) { }
        }


        [Fact]
        public void ObtenerCanceladosPorDia_ReturnsOk()
        {
            //Caso 3 - Patrullajes concluidos por día
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajesCancelados, PeriodoOpcion.UnDia);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerCanceladosPorMes_ReturnsOk()
        {
            //Caso 3 - Patrullajes cancelados por mes
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajesCancelados, PeriodoOpcion.UnMes);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerCancelados_ReturnsOk()
        {
            //Caso 3 - Patrullajes concluidos todos
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajesCancelados, PeriodoOpcion.Todos);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPatrullajePorDia_ReturnsOk()
        {
            //Caso 4 - Patrullajes  por día específico
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajeTodos, PeriodoOpcion.UnDia);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPatrullajePorMes_ReturnsOk()
        {
            //Caso 4 - Patrullajes  por mes específico
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajeTodos, PeriodoOpcion.UnMes);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPatrullaje_ReturnsOk()
        {
            //Caso 4 - Patrullajes  todos 
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PatrullajeTodos, PeriodoOpcion.Todos);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPropuestasTodas_Ordinario_ReturnsOk()
        {
            //Caso 5 - Ordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "PROGRAMADO", 2022, 09, 15, FiltroProgramaOpcion.PropuestasPendientes);
       
            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPropuestasTodas_Extraordinario_ReturnsOk()
        {
            //Caso 5 - Extraordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PropuestasPendientes);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPropuestasPendientes_Ordinario_ReturnsOk()
        {
            //Caso 6 - Ordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "PROGRAMADO", 2022, 09, 15, FiltroProgramaOpcion.PropuestasPendientes);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPropuestasPendientes_Extraordinario_ReturnsOk()
        {
            //Caso 6 - Extraordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PropuestasPendientes);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPropuestasAutorizadas_Ordinario_ReturnsOk()
        {
            //Caso 7 - Ordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "PROGRAMADO", 2022, 09, 15, FiltroProgramaOpcion.PropuestasAutorizadas);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPropuestasAutorizadas_Extraordinario_ReturnsOk()
        {
            //Caso 7 - Extraordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PropuestasAutorizadas);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPropuestasRechazadas_Ordinario_ReturnsOk()
        {
            //Caso 8 - Ordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "PROGRAMADO", 2022, 09, 15, FiltroProgramaOpcion.PropuestasRechazadas);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPropuestasRechazadas_Extraordinario_ReturnsOk()
        {
            //Caso 8 - Extraordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PropuestasRechazadas);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPropuestasEnviadas_Ordinario_ReturnsOk()
        {
            //Caso 9 - Ordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "PROGRAMADO", 2022, 09, 15, FiltroProgramaOpcion.PropuestasEnviadas);

            if (r.Count == 0) { }
        }

        [Fact]
        public void ObtenerPropuestasEnviadas_Extraordinario_ReturnsOk()
        {
            //Caso 9 - Extraordinario
            IProgramaService s = new ProgramasService(new ProgramaPatrullajeRepository(new ProgramaContext()));

            var r = s.ObtenerPorFiltro("TERRESTRE", 1, "EXTRAORDINARIO", 2022, 09, 15, FiltroProgramaOpcion.PropuestasEnviadas);

            if (r.Count == 0) { }
        }
    }
}