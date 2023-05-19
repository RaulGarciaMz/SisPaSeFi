using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class OficioAutorizacionService : IOficioAutorizacionService
    {
        private readonly IOficioAutorizacionRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public OficioAutorizacionService(IOficioAutorizacionRepo repo, IUsuariosParaValidacionQuery u)
        {
            _repo = repo;
            _user = u;
        }

        public async Task<List<string>> ObtenerInformacionParaOficioAutorizacion(string usuario, string password, int idPropuesta)
        {
            var res = new List<string>();

            var u = await _user.ObtenerUsuarioConfiguradorVerificaMd5Async(usuario,  password);
            if (u != null) 
            {
                var resFechas = "";
                var totalLineas = 0;
                var totalAutos = 0;

                var lineas = await _repo.ObtenerLineasDePropuestaAsync(idPropuesta);
                if (lineas != null && lineas.Count > 0) 
                {
                    var resLineasPtos = new List<string>();
                    var resAutos = new List<string>();

                    totalLineas = lineas.Count;
                    resLineasPtos.Add(totalLineas.ToString());

                    foreach (var line in lineas)
                    {
                        var lr = line.clave + "¦" + line.inicio + "¦" + line.fin;
                        resLineasPtos.Add(lr);
                    }

                    var vehiculos = await _repo.ObtenerVehiculosEnPropuestaAsync(idPropuesta);
                    if (vehiculos != null && vehiculos.Count > 0)
                    {
                        totalAutos = vehiculos.Count;
                        resAutos.Add(totalAutos.ToString());

                        foreach (var v in vehiculos)
                        {
                            var vr = v.placas + "¦" + v.numero + "¦" + v.tipo;
                            resAutos.Add(vr);
                        }
                    }

                    var props = await _repo.ObtenerPropuestaConResponsableAsync(idPropuesta);
                    if (props != null && props.Count > 0)
                    {
                        var p = props[0];

                        resFechas = p.fechaPatrullaje.ToString() + "¦" + p.fechaTermino.ToString() + "¦" + p.ubicacion + "¦" + 
                                    p.municipio + "¦" + p.estado + "¦" + p.nombre + "¦" + p.apellido1 + "¦" + p.apellido2 + "¦" + 
                                    p.InstalacionResponsable + "¦" + p.mun + "¦" + p.edo;
                    }

                    var resultado = new List<string>() { };
                    resultado.Add(totalLineas.ToString());

                    var linMenosHeader = resLineasPtos;
                    linMenosHeader.RemoveAt(0);
                    resultado.AddRange(linMenosHeader);

                    resultado.Add(totalAutos.ToString());
                    var autMenosHeader = resAutos;
                    autMenosHeader.RemoveAt(0);

                    resultado.AddRange(autMenosHeader );
                    resultado.Add(resFechas);

                    res = resultado;
                }
            }

            return res;
        }
    }
}
