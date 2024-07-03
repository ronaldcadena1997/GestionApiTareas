using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GestionApiTareas.Entities;
using GestionApiTareas.ViewModel;
using System.Data;
using System.Data.Entity;
using System.Globalization;

namespace GestionApiTareas.Servicios
{
    public class PermisosCuentasService
    {
        private static string _usuario;
        private static string _ip;

        private readonly ApplicationDbContext context;


        public PermisosCuentasService()
        {
        }

        public PermisosCuentasService(string usuario, string ip, ApplicationDbContext context)
        {
            _ip = ip;
            _usuario = usuario;
            this.context = context;
        }


        //    public Task<List<PermisosViewModel>> GetPermisosUsuarioRol() => context.PermisosPantalla
        //.Where(x => x.Activo)
        //.Select(x => new PermisosViewModel
        //{
        // nombrePantalla = x.Pantalla.NombrePantalla.ToString(),
        // nombreSeccion = x.SeccionPantalla.NombreSeccionPantalla.ToString(),
        // urlSeccion = x.SeccionPantalla.UrlPantalla.ToString()

        //}).ToListAsync();

        public List<PermisosViewModel> GetPermisosUsuaurios()
        {
            
            using (var contexto = new ApplicationDbContext())
            {
                try
                {
                    var idUsuario = contexto.Users.Where(x => x.Bloqueo != true && x.UserName == _usuario).FirstOrDefault();

                    var existingUserRole =  contexto.Set<IdentityUserRole<string>>().FirstOrDefault(ur => ur.UserId == idUsuario.Id);
                    if(existingUserRole != null) { 
                    var  result =  contexto.PermisosPantalla
                        .Where(x => x.Activo && x.idRol == existingUserRole.RoleId).OrderBy(v => v.posicionesPermisoPantalla)
                        .Select( y => new PermisosViewModel
                        {

                            nombrePantalla = y.Pantalla.NombrePantalla.ToString(),
                            iconoPantalla = y.Pantalla.IconoPantalla.ToString(),
                            nombreSeccion = y.SeccionPantalla.NombreSeccionPantalla.ToString(),
                            urlSeccion = y.SeccionPantalla.UrlPantalla.ToString()

                        }).ToList();

                    return result;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception e)
                {

                    return null;

                }

            }
        }




    }
}
