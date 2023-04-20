using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Driving
{
    public interface IRegistroDtoQuery
    {
        Task<UsuarioDtoRegistro> ObtenerUsuarioRegistradoAsync(UsuarioDtoForAutentication u, string pathLdap);
    }
}
