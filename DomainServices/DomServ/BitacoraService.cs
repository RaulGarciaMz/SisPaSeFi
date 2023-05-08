using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public BitacoraService(IBitacoraRepo repo, IUsuariosParaValidacionQuery uc)
        {
            _repo = repo;
            _user = uc;
        }

        public async Task AgregaBitacoraPorOpcionAsync(string opcion,int idReporte, int idEstado, string descripcion, string usuario)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                switch (opcion)
                {
                    case "INSTALACION":
                        await _repo.AgregaBitacoraInstalacionAsync(idReporte, idEstado, user.IdUsuario, descripcion);
                        break;
                    case "ESTRUCTURA":
                        await _repo.AgregaBitacoraEstructuraAsync(idReporte, idEstado, user.IdUsuario, descripcion);
                        break;
                }
            }
        }

        public async Task<List<BitacoraDto>> ObtenerBitacoraPorOpcionAsync(string opcion, int idReporte, string usuario)
        {
            var l = new List<BitacoraDto>();
            var lsta = new List<BitacoraSeguimientoVista>();

            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                switch (opcion)
                {
                    case "INSTALACION":
                        lsta = await _repo.ObtenerBitacoraInstalacionPorReporteAsync(idReporte);
                        break;
                    case "ESTRUCTURA":
                        lsta = await _repo.ObtenerBitacoraEstructuraPorReporteAsync(idReporte);
                        break;
                }

                if (lsta != null && lsta.Count > 0) 
                {
                    l = ConvierteListaBitacoraSeguimientoToListaDto(lsta);
                }
            }

            return l;
        }

        private BitacoraDto ConvierteBitacoraToDto(BitacoraSeguimientoVista b)
        {
            return new BitacoraDto() 
            {
                intIdBitacoraSeguimientoIncidencia = b.id_bitacora,
                intIdReporte = b.id_reporte,
                strUltimaActualizacion = b.ultimaactualizacion.ToString("yyyy-MM-dd HH:mm:ss"),
                intIdUsuario = b.id_usuario,
                strNombreCompletoUsuario = b.nombre + " " + b.apellido1 + " " + b.apellido2,
                strDescripcion = b.descripcion,
                intIdEstadoIncidencia = b.id_estadoincidencia,
                strDescripcionEstado = b.descripcionestado,
                strTipoIncidencia = b.tipoincidencia
            };
        }

        private List<BitacoraDto> ConvierteListaBitacoraSeguimientoToListaDto(List<BitacoraSeguimientoVista> lista)
        {
            var l = new List<BitacoraDto>();

            foreach (var b in lista) 
            {
                var bit = ConvierteBitacoraToDto(b);
                l.Add(bit);
            }

            return l;
        }
    }
}
