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

        public async Task<List<DocumentoPatrullaje>> ObtenerDocumentosAsync(DocumentoDto d) 
        {
            var l = new List<DocumentoPatrullaje>();
            var user = await _user.ObtenerUsuarioConfiguradorPorNombreAsync(d.Usuario);

            if (user != null)
            {
                l= await _repo.ObtenerDocumentosAsync(d.IdComandancia, d.Anio, d.Mes);
            }

            return l;
        }
    }
}
