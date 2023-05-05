using Domain.DTOs.catalogos;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class CatalogoConsultasService : ICatalogosConsultaService
    {
        private readonly ICatalogosConsultaRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public CatalogoConsultasService(ICatalogosConsultaRepo repo, IUsuariosConfiguradorQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<CatalogoGenerico>> ObtenerCatalogoPorOpcionAsync(string opcion, string usuario)
        {
            var cat = new List<CatalogoGenerico>();

            int dtoComplementario=0;

            if (opcion.Contains("-"))
            { 
                var splOpcion = opcion.Split("-");
                opcion = splOpcion[0];
                dtoComplementario = Int32.Parse( splOpcion[1]);
            }

            switch(opcion) 
            {
                case "RSF":
                    cat = await ObtenerComandanciaPorIdUsuarioAsync(usuario);
                    break;
                case "TodasLasRegiones":
                    cat = await ObtenerComandanciasAsync(usuario);
                    break;
                case "TipoPatrullaje":
                    cat = await ObtenerTiposPatrullajeAsync(usuario);
                    break;
                case "TipoVehiculo":
                    cat = await ObtenerTiposVehiculoAsync(usuario);
                    break;
                case "ClasificacionIncidencia":
                    cat = await ObtenerClasificacionesIncidenciaAsync(usuario);
                    break;
                case "Niveles":
                    cat = await ObtenerNivelesAsync(usuario);
                    break;
                case "ConceptosAfectacion":
                    cat = await ObtenerConceptosAfectacionAsync(usuario);
                    break;
                case "RegionesSDN":
                    cat = await ObtenerRegionesMilitaresEnRutasConDescVaciaAsync(usuario);
                    break;
                case "ResultadoPatrullaje":
                    cat = await ObtenerResultadosPatrullajeAsync(usuario);
                    break;
                case "EstadosDelPais":
                    cat = await ObtenerEstadosPaisAsync(usuario);
                    break;
                case "MunicipiosEstado":
                    cat = await ObtenerMunicipiosPorEstadoAsync(dtoComplementario, usuario);
                    break;
                case "ProcesosResponsables":
                    cat = await ObtenerProcesosResponsablesAsync(usuario);
                    break;
                case "GerenciaDivision":    
                    cat = await ObtenerGerenciaDivisionAsync(dtoComplementario, usuario);
                    break;
                case "TipoDocumento":
                    cat = await ObtenerTiposDocumentosAsync(usuario);
                    break;
                case "EstadosPatrullaje":
                    cat = await ObtenerEstadosPatrullajeAsync(usuario);
                    break;
                case "ApoyoPatrullaje":
                    cat = await ObtenerApoyosPatrullajeAsync(usuario);
                    break;
                case "InstalacionesDeComandancia":
                    cat = await ObtenerInstalacionesDeComandanciaAsync(dtoComplementario, usuario);
                    break;
                case "NivelRiesgo":
                    cat = await ObtenerNivelDeRiesgoAsync(usuario);
                    break;
                case "Hallazgo":
                    //cat = await ObtenerHallazgosAsync(usuario);
                    break;
                case "LocalidadMunicipio":
                    //cat = await ObtenerLocalidadesMunicipioAsync(dtoComplementario,usuario);
                    break;
                case "EstadosIncidencia":
                    //cat = await ObtenerEstadosIncidenciaAsync(usuario);
                    break;
                case "ComandanciasDeUnUsuario":
                    //cat = await ObtenerComandanciasDeUnUsuarioAsync(dtoComplementario, usuario);
                    break;
                case "GrupoCorreoDeUnUsuario":
                    //cat = await ObtenerGruposCorreoDeUnUsuarioAsync(dtoComplementario,usuario);
                    break;
                case "RolesDeUnUsuario":
                    //cat = await ObtenerRolesDeUnUsuarioAsync(dtoComplementario,usuario);
                    break;
                case "GruposCorreo":
                    //cat = await ObtenerGruposCorreoAsync(usuario);
                    break;
                case "MenusDeRol":
                    //cat = await ObtenerMenusDeRolAsync(dtoComplementario,usuario);
                    break;
            }
            
            return cat;           
        }

        public async Task<List<CatalogoGenerico>> ObtenerComandanciaPorIdUsuarioAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerComandanciaPorIdUsuarioAsync(user.IdUsuario);

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdComandancia,
                        Descripcion = c.Numero.ToString()
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerComandanciasAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerComandanciasAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdComandancia,
                        Descripcion = c.Numero.ToString()
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }
        
        public async Task<List<CatalogoGenerico>> ObtenerTiposPatrullajeAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerTiposPatrullajeAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdTipoPatrullaje,
                        Descripcion = c.Descripcion
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerTiposVehiculoAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerTiposVehiculoAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdTipoVehiculo,
                        Descripcion = c.DescripciontipoVehiculo
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerClasificacionesIncidenciaAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerClasificacionesIncidenciaAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdClasificacionIncidencia,
                        Descripcion = c.Descripcion
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerNivelesAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerNivelesAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdNivel,
                        Descripcion = c.DescripcionNivel
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async  Task<List<CatalogoGenerico>> ObtenerConceptosAfectacionAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerConceptosAfectacionAsync();

                foreach (var c in clas)
                {
                    var desc = c.Descripcion + "(" + c.PrecioUnitario.ToString() + "/" + c.Unidades + ")";
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdConceptoAfectacion,
                        Descripcion = desc
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerRegionesMilitaresEnRutasConDescVaciaAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerRegionesMilitaresEnRutanAsync();
                var ordenadas = clas.OrderBy(x => x).ToList();

                foreach (var c in ordenadas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c,
                        Descripcion = ""
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerResultadosPatrullajeAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerResultadosPatrullajeAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.Idresultadopatrullaje,
                        Descripcion = c.Descripcion
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerEstadosPaisAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerEstadosPaisAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdEstado,
                        Descripcion = c.Nombre
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }
            
        public async Task<List<CatalogoGenerico>> ObtenerMunicipiosPorEstadoAsync(int idEstado, string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerMunicipiosPorEstadoAsync(idEstado);

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdMunicipio,
                        Descripcion = c.Nombre
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerProcesosResponsablesAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerProcesosResponsablesAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdProcesoResponsable,
                        Descripcion = c.Nombre
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }
       
        public async Task<List<CatalogoGenerico>> ObtenerGerenciaDivisionAsync(int idProceso, string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var tabla = await _repo.ObtenerProcesosResponsablePorIdAsync(idProceso);

                if (tabla != null)
                {
                    var clas = await _repo.ObtenerCatalogoPorNombreTablaAync(tabla.Tabla);

                    foreach (var c in clas)
                    {
                        var row = new CatalogoGenerico()
                        {
                            Id = c.id,
                            Descripcion = c.nombre
                        };

                        cat.Add(row);
                    }
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerTiposDocumentosAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerTiposDocumentosAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = Convert.ToInt32(c.IdTipoDocumento),
                        Descripcion = c.Descripcion
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerEstadosPatrullajeAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerEstadosPatrullajeAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = Convert.ToInt32(c.IdEstadoPatrullaje),
                        Descripcion = c.DescripcionEstadoPatrullaje
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerApoyosPatrullajeAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerApoyosPatrullajeAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = Convert.ToInt32(c.IdApoyoPatrullaje),
                        Descripcion = c.Descripcion
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerInstalacionesDeComandanciaAsync(int idComandancia, string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerInstalacionesDeComandanciaAsync(idComandancia);

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = Convert.ToInt32(c.IdPunto),
                        Descripcion = c.Ubicacion
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

        public async Task<List<CatalogoGenerico>> ObtenerNivelDeRiesgoAsync(string usuario)
        {
            var cat = new List<CatalogoGenerico>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                var clas = await _repo.ObtenerNivelDeRiesgoAsync();

                foreach (var c in clas)
                {
                    var row = new CatalogoGenerico()
                    {
                        Id = c.IdNivelRiesgo,
                        Descripcion = c.Nivel
                    };

                    cat.Add(row);
                }
            }

            return cat;
        }

    }
}