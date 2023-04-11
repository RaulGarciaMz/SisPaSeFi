using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driven.Repositories
{
    public interface ICatalogosConsultaRepo: IComandanciasQuery, ITipoPatrullajeQuery, ITipoVehiculoQuery, IClasificacionIncidenciaQuery, 
        INivelesQuery, IConceptoAfectacionQuery, IRegionEnRutaQuery, IEstadoPaisQuery, IProcesoResponsableQuery, ITipoDocumentoQuery, 
        IMunicipioQuery, IGerenciaDivisionQuery, IResultadoPatrullajeQuery, IEstadoPatrullajeQuery, IApoyoPatrullajeQuery, IInstalacionesQuery
    {
    }
}