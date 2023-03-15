using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    [Keyless]
    public class DocumentosVista
    {
        public long id_documentoPatrullaje { get; set; }
        public long? id_referencia { get; set; }
        public long id_tipoDocumento { get; set; }
        public int id_comandancia { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaReferencia { get; set; }
        public string rutaArchivo { get; set; }
        public string nombreArchivo { get; set; }
        public string descripcion { get; set; }
        public int id_usuario { get; set; }
        public string descripciontipodocumento { get; set; }
        public string usuario { get; set; }
    }
}