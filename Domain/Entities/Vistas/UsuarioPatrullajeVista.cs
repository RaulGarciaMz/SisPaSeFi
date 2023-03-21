using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vistas
{
    public class UsuarioPatrullajeVista
    {
        public int id_usuario { get; set; }
        public string usuario_nom { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string correoelectronico { get; set; }
        public string cel { get; set; }
        public int configurador { get; set; }
    }
}