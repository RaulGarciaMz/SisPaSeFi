using Domain.Entities;
using Domain.Ports.Driven.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class RegistroEntradaUsuarioRepository : IRegistroEntradaUsuarioRepo
    {
        protected readonly RegistroEntradaUsuarioContext _registroContext;

        public RegistroEntradaUsuarioRepository(RegistroEntradaUsuarioContext registroContext)
        {
            _registroContext = registroContext ?? throw new ArgumentNullException(nameof(registroContext));
        }

        public async Task AumentaIntentosDeUsuarioEnMemoriaAsync(string usuario)
        {
            var u = await _registroContext.Usuarios.Where(x => x.UsuarioNom == usuario).FirstOrDefaultAsync();

            if (u != null)
            {
                u.Intentos = u.Intentos + 1;

                _registroContext.Usuarios.Update(u);
            }
        }

        public async Task ReseteaIntentosDeUsuarioEnMemoriaAsync(string usuario)
        {
            var u = await _registroContext.Usuarios.Where(x => x.UsuarioNom == usuario).FirstOrDefaultAsync();

            if (u != null)
            {
                u.Intentos = 0;

                _registroContext.Usuarios.Update(u);
            }
        }

        public async Task<bool> BloqueaUsuarioEnMemoriaAsync(string usuario)
        {
            var bloqueo = false;
            var u = await _registroContext.Usuarios.Where(x => x.UsuarioNom == usuario).FirstOrDefaultAsync();

            if (u != null)
            {
                if (u.Intentos > 2)
                {
                    u.Bloqueado = 1;

                    _registroContext.Usuarios.Update(u);
                    bloqueo = true;
                }
            }

            return bloqueo;
        }

        public async Task ActualizaTotalDeAccesosEnMemoriaAsync(int accesos)
        {
            var hoy = DateTime.UtcNow;
            var anio = hoy.Year;
            var mes = hoy.Month;
            var elacceso = await _registroContext.Accesos.Where(x => x.Fecha.Value.Year == anio && x.Fecha.Value.Month == mes).SingleAsync();

            elacceso.Totalaccesos = accesos;

            _registroContext.Accesos.Update(elacceso);
        }

        public void AgregaAccesoEnMemoria()
        {
            var acceso = new Acceso()
            {
                Fecha = DateTime.UtcNow,
                Totalaccesos = 1
            };

            _registroContext.Accesos.Add(acceso);
        }

        public async Task ActualizaUltimoAccesoDeUsuarioEnMemoriaAsync(string usuario)
        {
            var user = await _registroContext.Usuarios.Where(x => x.UsuarioNom == usuario).SingleAsync();

            user.EstampaTiempoAceptacionUso = DateTime.UtcNow;

            _registroContext.Usuarios.Update(user);
        }

        public async Task AgregaSesionDeUsuarioEnMemoriaAsync(string usuario)
        {
            var user = await _registroContext.Usuarios.Where(x => x.UsuarioNom == usuario).SingleAsync();

            var sesion = new Sesion()
            {
                IdUsuario = user.IdUsuario,
                EstampaTiempoInicio = user.EstampaTiempoUltimoAcceso,
                EstampaTiempoTerminacion = DateTime.UtcNow
            };

            _registroContext.Sesiones.Add(sesion);
        }

        public async Task RegistraEventoDeUsuarioAsync(int numSesion, string resultado)
        {
            var q = new Evento()
            {
                IdSesion = numSesion,
                DescripcionEvento = resultado,
                EstampaTiempo = DateTime.Now
            };

            _registroContext.Eventos.Add(q);

            await _registroContext.SaveChangesAsync();
        }

        public async Task RegistraFinDeSesionAsync(int numSesion)
        {
            var s = await _registroContext.Sesiones.Where(x => x.IdSesion == numSesion).SingleAsync();

            s.EstampaTiempoTerminacion = DateTime.UtcNow;
            _registroContext.Sesiones.Update(s);

            await _registroContext.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _registroContext.SaveChangesAsync() >= 0);
        }

        public async Task<int> ObtenerUltimaSesionDeUsuarioAsync(int idUsuario)
        {
            int numSesion = 0;
            var user = await _registroContext.Usuarios.Where(x => x.IdUsuario == idUsuario).SingleAsync();

            if (user != null)
            {
                var sesion = await _registroContext.Sesiones.Where(x => x.IdUsuario == idUsuario && x.EstampaTiempoInicio == user.EstampaTiempoUltimoAcceso).ToListAsync();
                numSesion = sesion[0].IdSesion;
            }

            return numSesion;
        }

        public string ObtenerPaginaDeInicioDeUsuario(int idUsuario)
        {
            string sqlQuery = @"SELECT c.* 
                                FROM ssf.usuariorol a
                                JOIN ssf.roles b ON a.id_rol = b.id_rol
                                JOIN ssf.menus c ON b.idmenu = c.idmenu
                                WHERE a.id_usuario = @pIdUsuario";

            object[] parametros = new object[]
            {
                new SqlParameter("@pIdUsuario", idUsuario)
            };

            var pagInicio = _registroContext.Menues.FromSqlRaw(sqlQuery, parametros).ToList();

            if (pagInicio != null && pagInicio.Count > 0)
            {
                return pagInicio[0].Liga;
            }

            return string.Empty;
        }

        public async Task<int?> ObtenerAvisoLegalDeUsuarioAsync(string usuario)
        {
            var s = await _registroContext.Usuarios.Where(x => x.UsuarioNom == usuario).SingleOrDefaultAsync();

            return s.AceptacionAvisoLegal;
        }

        public async Task ActualizaAvisoLegalDeUsuarioAsync(string usuario)
        {
            var s = await _registroContext.Usuarios.Where(x => x.UsuarioNom == usuario).SingleAsync();

            s.AceptacionAvisoLegal = 1;
            s.EstampaTiempoAceptacionUso = DateTime.UtcNow;

            _registroContext.Usuarios.Update(s);

            await _registroContext.SaveChangesAsync();
        }

        public async Task ActualizaCorreoElectronicoDeUsuarioAsync(string usuario, string correo, int notificar)
        {
            var s = await _registroContext.Usuarios.Where(x => x.UsuarioNom == usuario).SingleAsync();

            s.CorreoElectronico = correo;
            s.NotificarAcceso = notificar;

            _registroContext.Usuarios.Update(s);

            await _registroContext.SaveChangesAsync();
        }

        public async Task<List<Acceso>> ObtenerAccesosAsync()
        {
            return await _registroContext.Accesos.ToListAsync();
        }

    }
}
