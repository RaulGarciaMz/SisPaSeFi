using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;

namespace DomainServices.DomServ
{
    public class DocumentosService : IDocumentoService
    {
        private readonly IDocumentosRepo _repo;
        private readonly IUsuariosParaValidacionQuery _user;

        public DocumentosService(IDocumentosRepo repo, IUsuariosParaValidacionQuery uc)
        {
            _repo = repo;
            _user = uc;
        }

        public async Task AgregarAsync(DocumentoDtoForCreate d)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(d.strUsuario);

            await _repo.AgregarAsync(d.intIdReferencia, d.intIdTipoDocumento, d.intIdComandancia, d.strRutaArchivo, d.strNombreArchivo, d.strDescripcionArchivo, d.strFechaReferencia, d.intIdUsuario);
        }

        public async Task<List<DocumentoDto>> ObtenerDocumentosAsync(string opcion, string criterio, int anio, int mes, string usuario) 
        {
            var l = new List<DocumentoDto>();
            var docs = new List<DocumentosVista>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(usuario);

            if (user != null)
            {
                switch (opcion)
                {
                    case "DocumentosPatrullaje":
                        var idComandancia = Int32.Parse(criterio);
                         docs = await _repo.ObtenerDocumentosPatrullajeAsync(idComandancia, anio, mes);
                        
                        break;
                    case "DocumentosDeUnUsuario":
                        switch (criterio)
                        {
                            case "TODO":
                                docs = await _repo.ObtenerDocumentosDeUnUsuarioTodosAsync(user.IdUsuario);
                                break;
                            case "MES":
                                docs = await _repo.ObtenerDocumentosDeUnUsuarioMesAsync(user.IdUsuario, anio, mes);
                                break;
                        }
                        break;
                    case "DocumentosParaUnUsuario":
                        switch (criterio)
                        {
                            case "TODO":
                                docs = await _repo.ObtenerDocumentosParaUnUsuarioTodosAsync(user.IdUsuario);
                                break;
                            case "MES":
                                docs = await _repo.ObtenerDocumentosParaUnUsuarioMesAsync(user.IdUsuario, anio, mes);
                                break;
                        }
                        break;
                }

                l = ConvierteListaDocumentosToDto(docs);

            }

            return l;
        }

        private DocumentoDto ConvierteDocumentoToDto(DocumentosVista d)
        {
            var doc = new DocumentoDto() 
            {
                intIdDocumento = d.id_documentoPatrullaje,
                intIdReferencia= d.id_referencia,
                intIdTipoDocumento = d.id_tipoDocumento,
                intIdComandancia = d.id_comandancia,
                strFechaRegistro = d.fechaRegistro,
                strFechaReferencia = d.fechaReferencia,
                strRutaArchivo = d.rutaArchivo,
                strNombreArchivo = d.nombreArchivo,
                strDescripcionArchivo = d.descripcion,
                intIdUsuario = d.id_usuario,
                strDescripcionTipoDocumento = d.descripciontipodocumento,
                strNombreCompletoUsuario = d.usuario,
                strCorreoElectronico = d.correoelectronico
            };

            return doc;
        }

        private List<DocumentoDto> ConvierteListaDocumentosToDto(List<DocumentosVista> documentos) 
        {
            var ldto = new List<DocumentoDto>();

            foreach (var item in documentos)
            {
                var d = ConvierteDocumentoToDto(item);

                ldto.Add(d);
            }

            return ldto;
        }
    }
}
