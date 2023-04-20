using Domain.Entities;
using Domain.Entities.Vistas;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class DocumentosRepository : IDocumentosRepo
    {
        protected readonly DocumentosContext _documentosContext;

        public DocumentosRepository(DocumentosContext documentoContext)
        {
            _documentosContext = documentoContext ?? throw new ArgumentNullException(nameof(documentoContext));
        }

        public async Task<List<DocumentosVista>> ObtenerDocumentosPatrullajeAsync(int idComandancia, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_documentopatrullaje, a.id_referencia, a.id_tipodocumento, a.id_comandancia, a.fecharegistro, 
                                       a.fechareferencia, a.rutaarchivo, a.nombrearchivo, a.descripcion, a.id_usuario, 
                                	   b.descripcion descripciontipodocumento, CONCAT(c.nombre, ' ', c.apellido1) usuario,
                                       c.correoelectronico
                                FROM ssf.documentospatrullaje a
                                JOIN ssf.tipodocumento b ON a.id_tipodocumento = b.id_tipodocumento
                                JOIN ssf.usuarios c ON a.id_usuario = c.id_usuario
                                WHERE a.id_comandancia = @pCriterio 
                                AND MONTH(a.fechareferencia)= @pMes AND YEAR(a.fechareferencia)= @pAnio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pCriterio", idComandancia),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes)
            };

            return await _documentosContext.DocumentosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<DocumentosVista>> ObtenerDocumentosDeUnUsuarioTodosAsync(int idUsuario)
        {
            string sqlQuery = @"SELECT a.id_documentopatrullaje, a.id_referencia, a.id_tipodocumento, a.id_comandancia, a.fecharegistro, 
                                       a.fechareferencia, a.rutaarchivo, a.nombrearchivo, a.descripcion, a.id_usuario, 
                                	   b.descripcion descripciontipodocumento, CONCAT(c.nombre, ' ', c.apellido1) usuario,
                                       c.correoelectronico
                                FROM ssf.documentospatrullaje a
                                JOIN ssf.tipodocumento b ON a.id_tipodocumento = b.id_tipodocumento
                                JOIN ssf.usuarios c ON a.id_usuario = c.id_usuario
                                WHERE a.id_usuario = @pUsuario ";

            object[] parametros = new object[]
            {
                new SqlParameter("@pUsuario", idUsuario)
            };

            return await _documentosContext.DocumentosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<DocumentosVista>> ObtenerDocumentosDeUnUsuarioMesAsync(int idUsuario, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_documentopatrullaje, a.id_referencia, a.id_tipodocumento, a.id_comandancia, a.fecharegistro, 
                                       a.fechareferencia, a.rutaarchivo, a.nombrearchivo, a.descripcion, a.id_usuario, 
                                	   b.descripcion descripciontipodocumento, CONCAT(c.nombre, ' ', c.apellido1) usuario,
                                       c.correoelectronico
                                FROM ssf.documentospatrullaje a
                                JOIN ssf.tipodocumento b ON a.id_tipodocumento = b.id_tipodocumento
                                JOIN ssf.usuarios c ON a.id_usuario = c.id_usuario
                                WHERE a.id_usuario = @pUsuario 
                                  AND MONTH(a.fechareferencia)=@pMes AND YEAR(a.fechareferencia)=@pAnio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pUsuario", idUsuario),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes)
            };

            return await _documentosContext.DocumentosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<DocumentosVista>> ObtenerDocumentosParaUnUsuarioTodosAsync(int idUsuario)
        {
            string sqlQuery = @"SELECT a.id_documentopatrullaje, a.id_referencia, a.id_tipodocumento, a.id_comandancia, a.fecharegistro, 
                                       a.fechareferencia, a.rutaarchivo, a.nombrearchivo, a.descripcion, a.id_usuario, 
                                	   b.descripcion descripciontipodocumento, CONCAT(c.nombre, ' ', c.apellido1) usuario,
                                       c.correoelectronico
                                FROM ssf.documentospatrullaje a
                                JOIN ssf.tipodocumento b ON a.id_tipodocumento = b.id_tipodocumento
                                JOIN ssf.usuarios c ON a.id_usuario = c.id_usuario
                                JOIN ssf.usuariodocumento d ON a.id_documentopatrullaje=d.id_documentopatrullaje
                                WHERE d.id_usuario = @pUsuario ";

            object[] parametros = new object[]
            {
                new SqlParameter("@pUsuario", idUsuario)
            };

            return await _documentosContext.DocumentosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task<List<DocumentosVista>> ObtenerDocumentosParaUnUsuarioMesAsync(int idUsuario, int anio, int mes)
        {
            string sqlQuery = @"SELECT a.id_documentopatrullaje, a.id_referencia, a.id_tipodocumento, a.id_comandancia, a.fecharegistro, 
                                       a.fechareferencia, a.rutaarchivo, a.nombrearchivo, a.descripcion, a.id_usuario, 
                                	   b.descripcion descripciontipodocumento, CONCAT(c.nombre, ' ', c.apellido1) usuario,
                                       c.correoelectronico
                                FROM ssf.documentospatrullaje a
                                JOIN ssf.tipodocumento b ON a.id_tipodocumento = b.id_tipodocumento
                                JOIN ssf.usuarios c ON a.id_usuario = c.id_usuario
                                JOIN ssf.usuariodocumento d ON a.id_documentopatrullaje=d.id_documentopatrullaje
                                WHERE d.id_usuario = @pUsuario 
                                AND MONTH(a.fechareferencia)=@pMes AND YEAR(a.fechareferencia)=@pAnio";

            object[] parametros = new object[]
            {
                new SqlParameter("@pUsuario", idUsuario),
                new SqlParameter("@pAnio", anio),
                new SqlParameter("@pMes", mes)
            };

            return await _documentosContext.DocumentosVista.FromSqlRaw(sqlQuery, parametros).ToListAsync();
        }

        public async Task AgregarAsync(long idReferencia, long idTipoDocumento, int idComandancia, string rutaArchivo, string nombreArchivo, string descripcionArchivo, DateTime fechaReferencia, int idUsuario)
        {

            var doc = new DocumentoPatrullaje() 
            { 
                IdReferencia = idReferencia,
                IdTipoDocumento = idTipoDocumento,
                IdComandancia = idComandancia,
                IdUsuario = idUsuario,
                RutaArchivo = rutaArchivo,
                NombreArchivo = nombreArchivo,
                Descripcion = descripcionArchivo,
                FechaReferencia = fechaReferencia,
                FechaRegistro = DateTime.UtcNow
            };

            _documentosContext.DocumentosPatrullaje.Add(doc);

            await _documentosContext.SaveChangesAsync();

        }

    }
}
