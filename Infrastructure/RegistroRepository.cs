using Domain.Entities;
using Domain.Ports.Driven.Repositories;
using Microsoft.EntityFrameworkCore;
using SqlServerAdapter.Data;

namespace SqlServerAdapter
{
    public class RegistroRepository : IRegistroRepo
    {
        protected readonly RegistroContext _registroContext;

        public RegistroRepository(RegistroContext rolesContext)
        {
            _registroContext = rolesContext ?? throw new ArgumentNullException(nameof(rolesContext));
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
                var sesion = await _registroContext.Sesiones.Where(x => x.IdUsuario == idUsuario && x.EstampaTiempoInicio == user.EstampaTiempoUltimoAcceso).SingleAsync();
                numSesion = sesion.IdSesion;
            }

            return numSesion;
        }

        public string ObtenerPaginaDeInicioDeUsuario(int idUsuario)
        {
            var q = (from ur in _registroContext.UsuariosRol
                     join ro in _registroContext.Roles on ur.IdRol equals ro.IdRol
                     join me in _registroContext.Menues on ro.IdMenu equals me.IdMenu
                     where ur.IdUsuario == idUsuario
                     select new
                     {
                         me.Liga
                     }).Single().ToString();

            return q ?? "";
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
