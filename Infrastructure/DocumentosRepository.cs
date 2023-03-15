using Domain.DTOs.catalogos;
using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServerAdapter
{
    public class DocumentosRepository : IDocumentosRepo
    {
        protected readonly DocumentosContext _documentosContext;

        public DocumentosRepository(DocumentosContext documentoContext)
        {
            _documentosContext = documentoContext ?? throw new ArgumentNullException(nameof(documentoContext));
        }

        public async Task<List<DocumentosVista>> ObtenerDocumentosAsync(int idComandancia, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_documentopatrullaje, a.id_referencia, a.id_tipodocumento, a.id_comandancia, a.fecharegistro, 
                                       a.fechareferencia, a.rutaarchivo, a.nombrearchivo, a.descripcion, a.id_usuario, 
                                	   b.descripcion descripciontipodocumento, CONCAT(c.nombre, ' ', c.apellido1) usuario
                                FROM ssf.documentospatrullaje a
                                JOIN ssf.tipodocumento b ON a.id_tipodocumento = b.id_tipodocumento
                                JOIN ssf.usuarios c ON a.id_usuario = c.id_usuario
                                WHERE a.id_comandancia = @pComandancia 
                                AND MONTH(a.fechareferencia)= @pMes AND YEAR(a.fechareferencia)= @pAnio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pComandancia", idComandancia),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes)
            };

            return await _documentosContext.DocumentosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }
    }
}
