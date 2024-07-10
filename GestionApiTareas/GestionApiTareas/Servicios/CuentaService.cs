using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Entity;
using GestionApiTareas.DTOs;
using GestionApiTareas.Entities;
using GestionApiTareas.ViewModel;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace GestionApiTareas.Servicios
{


    public class CuentaService
    {

        private readonly UserManager<ApplicationUser> context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;
        private static string _usuario;
        private static string _ip;
        public CuentaService(UserManager<ApplicationUser> context, RoleManager<IdentityRole>  roleManager , ApplicationDbContext contextRol, string usuario, string ip)
        {
             _ip = ip;
            _usuario = usuario;
            this.context = context;
            this.roleManager = roleManager;
            this._context = contextRol;
        }

      
        public UsuarioLista GetUsuarioUnico(string usuariosid)
        {
            try
            {
                var idUsuarios = context.Users.Where(x => x.Bloqueo != true && x.UserName == usuariosid).FirstOrDefault().Id;

                UsuarioLista usuarios = context.Users

                    .Where(x => !x.Bloqueo && x.Id == idUsuarios)
                    .Select(x => new UsuarioLista

                    {
                        id = x.Id,
                        nombre = x.FirstName != null ? x.FirstName : "",
                        apellido = x.LastName,
                        correo = x.Email,
                        telefono = x.PhoneNumber,
                        tipoRol = roleManager.Roles.Where(d => d.Id == _context.Set<IdentityUserRole<string>>().Where(ur => ur.UserId == x.Id).FirstOrDefault().RoleId).FirstOrDefault() != null
                        ? roleManager.Roles.Where(d => d.Id == _context.Set<IdentityUserRole<string>>().Where(ur => ur.UserId == x.Id).FirstOrDefault().RoleId).FirstOrDefault().Name : "Sin Rol"
                    })
                    .OrderBy(c => c.nombre)
                    .FirstOrDefault();
                return usuarios;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public List<UsuarioLista> GetUsuario()
        {
            try { 
            List<UsuarioLista> usuarios =  context.Users
                 
                .Where(x => !x.Bloqueo )
                .Select(x => new UsuarioLista

                {
                    id = x.Id,
                    nombre = x.FirstName != null ? x.FirstName : "",
                    apellido = x.LastName,
                    correo = x.Email,
                    telefono = x.PhoneNumber,
                    tipoRol = roleManager.Roles.Where(d => d.Id == _context.Set<IdentityUserRole<string>>().Where(ur => ur.UserId == x.Id).FirstOrDefault().RoleId).FirstOrDefault() != null
                    ? roleManager.Roles.Where(d => d.Id == _context.Set<IdentityUserRole<string>>().Where(ur => ur.UserId == x.Id).FirstOrDefault().RoleId).FirstOrDefault().Name : "Sin Rol"
                })
                .OrderBy(c => c.nombre)
                .ToList();
                return usuarios;
            }
            catch (Exception e)
            {
                return null;
            }
         
        }


              public List<RolesLista> GetRoles()
        {
            List<RolesLista> roles =  roleManager.Roles
                .AsQueryable()
                .Select(x => new RolesLista
                {
                    id = x.Id,
                    descripcion = x.Name
                })
                .ToList(); // Si se trata de Entity Framework Core

            return roles;
        }


        public async Task<bool> EliminarUsuario(EliminarUsuario usuarios)
        {

            try { 
            var existingUsuario = await context.FindByIdAsync(usuarios.id);
        
                existingUsuario.Bloqueo = true;
             
             
                // Actualiza otras propiedades según sea necesario

                var result = await context.UpdateAsync(existingUsuario); // Actualiza el usuario existente



                return result.Succeeded; // Indica si la operación de actualización fue exitosa
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public async Task<bool> SaveUsuarios(UsuarioLista usuarios)
        {
            var existingUsuario =  context.Users.Where(x => x.Id == usuarios.id).FirstOrDefault();
            if (existingUsuario == null)
            {

                var nuevoUsuario = new ApplicationUser
                {
                 
                    FirstName = usuarios.nombre,
                    LastName = usuarios.apellido,
                    UserName = usuarios.correo,
                    Email = usuarios.correo,
                    PhoneNumber = usuarios.telefono
                    // Asigna otras propiedades según sea necesario
                };
                var result = await context.CreateAsync(nuevoUsuario, usuarios.contrasenia);

                if (result.Succeeded == true)
                {

                    var idUsuario =  context.Users.Where(x => x.Bloqueo != true && x.UserName == usuarios.correo).FirstOrDefault();

                    var existingUserRole = _context.Set<IdentityUserRole<string>>().FirstOrDefault(ur => ur.UserId == idUsuario.Id);
                    if (existingUserRole == null && usuarios.tipoRol != null)
                    {
                        var userRole = new IdentityUserRole<string>
                        {
                            UserId = idUsuario.Id,
                            RoleId =usuarios.tipoRol
                        };
                        _context.Set<IdentityUserRole<string>>().Add(userRole);


                    }
                    else
                    {
                        if (existingUserRole != null && usuarios.tipoRol != null)
                        {
                            _context.Set<IdentityUserRole<string>>().Remove(existingUserRole);

                            var newUserRole = new IdentityUserRole<string>
                            {
                                UserId = idUsuario.Id,
                                RoleId = usuarios.tipoRol
                            };
                            _context.Set<IdentityUserRole<string>>().Add(newUserRole);
                        }
                    }

                    await _context.SaveChangesAsync();

                }

                return result.Succeeded; // Indica si la operación de creación fue exitosa




            }
            else
            {
                existingUsuario.FirstName = usuarios.nombre;
                existingUsuario.LastName = usuarios.apellido;
                existingUsuario.Email = usuarios.correo;
                existingUsuario.PhoneNumber = usuarios.telefono;
                existingUsuario.Bloqueo = false;
                if(usuarios.contrasenia != null && usuarios.contrasenia != "") { 
                existingUsuario.PasswordHash = context.PasswordHasher.HashPassword(existingUsuario, usuarios.contrasenia);
                }
                // Actualiza otras propiedades según sea necesario

                var result = await context.UpdateAsync(existingUsuario); // Actualiza el usuario existente


                var idUsuarios =  context.Users.Where(x => x.Bloqueo != true && x.UserName == usuarios.correo).FirstOrDefault();

                var existingUserRole = _context.Set<IdentityUserRole<string>>().FirstOrDefault(ur => ur.UserId == idUsuarios.Id);
                if (existingUserRole == null && usuarios.tipoRol != null)
                {
                    var userRole = new IdentityUserRole<string>
                    {
                        UserId = idUsuarios.Id,
                        RoleId = usuarios.tipoRol
                    };
                    _context.Set<IdentityUserRole<string>>().Add(userRole);


                }
                else
                {
                    if (existingUserRole != null && usuarios.tipoRol != null)
                    {
                        _context.Set<IdentityUserRole<string>>().Remove(existingUserRole);

                        var newUserRole = new IdentityUserRole<string>
                        {
                            UserId = idUsuarios.Id,
                            RoleId = usuarios.tipoRol
                        };
                        _context.Set<IdentityUserRole<string>>().Add(newUserRole);
                    }
                }

                await _context.SaveChangesAsync();

                return result.Succeeded; // Indica si la operación de actualización fue exitosa

            }
        }
        public async Task<bool> SaveUsuarioRoles(UsuarioRoles rolesuUser)
        {
          
            try
            {
                var existingUserRole = _context.Set<IdentityUserRole<string>>().FirstOrDefault(ur => ur.UserId == rolesuUser.idUser);
                if (existingUserRole == null)
                {
                    var userRole = new IdentityUserRole<string>
                    {
                        UserId = rolesuUser.idUser,
                        RoleId = rolesuUser.idRol
                    };
                    _context.Set<IdentityUserRole<string>>().Add(userRole);
                    
                   
                }
                else
                {
                    _context.Set<IdentityUserRole<string>>().Remove(existingUserRole);

                    var newUserRole = new IdentityUserRole<string>
                    {
                        UserId = rolesuUser.idUser,
                        RoleId = rolesuUser.idRol
                    };
                    _context.Set<IdentityUserRole<string>>().Add(newUserRole);
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        

        public async Task<bool> SaveRoles(RolesLista roles)
        {
            var existingRole = await roleManager.FindByIdAsync(roles.id);
            if (existingRole == null)
            {
                // Crear un nuevo rol si no existe
                var newRole = new IdentityRole { Name = roles.descripcion.ToUpper() };
                var result = await roleManager.CreateAsync(newRole);

                return result.Succeeded;
            }
            else
            {
                existingRole.Name = roles.descripcion.ToUpper(); // Actualizar el nombre del rol

                var result = await roleManager.UpdateAsync(existingRole);

                return result.Succeeded;

            }
        }

        public async Task<bool> DeleteRoles(RolesLista roles)
        {
            var existingRole = await roleManager.FindByIdAsync(roles.id);
            if (existingRole != null)
            {
                var result = await roleManager.DeleteAsync(existingRole);

                return result.Succeeded;
            }
            else
            {
                return false;

            }
        }


    }
}
