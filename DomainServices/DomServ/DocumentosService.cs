using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driven;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Ports.Driving;
using Domain.DTOs;
using Domain.Entities.Vistas;
using Domain.DTOs.catalogos;

namespace DomainServices.DomServ
{
    public class DocumentosService : IDocumentoService
    {
        private readonly IDocumentosRepo _repo;
        private readonly IUsuariosConfiguradorQuery _user;

        public DocumentosService(IDocumentosRepo repo, IUsuariosConfiguradorQuery uc)
        {
            _repo = repo;
            _user = uc;
        }

        public async Task AgregarAsync(DocumentoDtoForCreate d)
        {
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(d.Usuario);

            await _repo.AgregarAsync(d.IdReferencia, d.IdTipoDocumento, d.IdComandancia, d.RutaArchivo, d.NombreArchivo, d.Descripcion, d.FechaReferencia, d.IdUsuario);
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
                IdDocumentoPatrullaje = d.id_documentoPatrullaje,
                IdReferencia= d.id_referencia,
                IdTipoDocumento = d.id_tipoDocumento,
                IdComandancia = d.id_comandancia,
                FechaRegistro = d.fechaRegistro,
                FechaReferencia = d.fechaReferencia,
                RutaArchivo = d.rutaArchivo,
                NombreArchivo = d.nombreArchivo,
                Descripcion = d.descripcion,
                IdUsuario = d.id_usuario,
                DescripcionTipoDocumento = d.descripciontipodocumento,
                Usuario = d.usuario,
                CorreoElectronico = d.correoelectronico
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
