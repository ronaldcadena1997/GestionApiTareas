using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionApiTareas.DTOs;
using GestionApiTareas.Entities;
using GestionApiTareas.Servicios;
using GestionApiTareas.ViewModel;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace GestionApiTareas.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly HashService hashService;
        private readonly IDataProtector dataProtector;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        protected CuentaService _service;
    
        public CuentasController(UserManager<ApplicationUser> userManager,
                                 IConfiguration configuration,
                                 SignInManager<ApplicationUser> signInManager,
                                 IDataProtectionProvider dataProtectionProvider,
                                 HashService hashService,
                                 RoleManager<IdentityRole> roleManager,
                                 ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.hashService = hashService;
            dataProtector = dataProtectionProvider.CreateProtector("algun_valor_aleatorio");
            _roleManager = roleManager;
            _context = context;
            this._service = new CuentaService(userManager , roleManager , _context);
        }

        [HttpGet("encriptar")]
        public ActionResult Encriptar()
        {
            var textoPlano = "Geovanny Andrade";
            var textoCifrado = dataProtector.Protect(textoPlano);
            var textoDesencriptado = dataProtector.Unprotect(textoCifrado);
            return Ok(new
            {
                textoPlano,
                textoCifrado,
                textoDesencriptado
            });
        }
        

              [HttpPost("GuardarUsuarioRol")]
        public async Task<IActionResult> GuardarUsuarioRol(UsuarioRoles usuario) => Ok(await _service.SaveUsuarioRoles(usuario));
        [HttpPost("EliminarUsuario")]
        public async Task<IActionResult> EliminarUsuario(EliminarUsuario usuario) => Ok(await _service.EliminarUsuario(usuario));



        [HttpGet("ListarUsuario")]
        public async Task<IActionResult> GetUsuarios() => Ok( _service.GetUsuario());


        [HttpPost("GuardarUsuarios")]
        public async Task<IActionResult> GuardarUsuarios(UsuarioLista usuario) => Ok(await _service.SaveUsuarios(usuario));


        [HttpGet("ListarRoles")]
        public async Task<IActionResult> GetListarRoles() => Ok( _service.GetRoles());

        [HttpPost("NuevoRol")]
        public async Task<IActionResult> NuevoRol(RolesLista roles) => Ok(await _service.SaveRoles(roles));



        [HttpPost("EliminarRol")]
        public async Task<IActionResult> EliminarRol(RolesLista roles) => Ok(await _service.DeleteRoles(roles));



        [HttpGet("encriptarPorTiempo")]
        public ActionResult EncriptarPorTiempo()
        {
            var protectoLimitadoPorTiempo = dataProtector.ToTimeLimitedDataProtector();

            var textoPlano = "Geovanny Andrade";
            var textoCifrado = protectoLimitadoPorTiempo.Protect(textoPlano, lifetime: TimeSpan.FromSeconds(5));
            Thread.Sleep(TimeSpan.FromSeconds(6));
            var textoDesencriptado = protectoLimitadoPorTiempo.Unprotect(textoCifrado);
            return Ok(new
            {
                textoPlano,
                textoCifrado,
                textoDesencriptado
            });
        }

        [HttpGet("hash/{textoPlano}")]
        public ActionResult RealizarHash(string textoPlano)
        {
            var result1 = hashService.Hash(textoPlano);
            var result2 = hashService.Hash(textoPlano);
            return Ok(new
            {
                textoPlano,
                hash1 = result1,
                hash2 = result2,
            });
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = new ApplicationUser
            {
                UserName = credencialesUsuario.Email,
                Email = credencialesUsuario.Email,
                FirstName = credencialesUsuario.FirstName,
                LastName = credencialesUsuario.LastName

            };
                var resultado = await userManager.CreateAsync(usuario , credencialesUsuario.Password);
            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);

            }
            else {
                return BadRequest(resultado.Errors);
            }



        }
           

        [HttpPost("login", Name = "loginUsuario")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario) =>
            (await signInManager.PasswordSignInAsync(credencialesUsuario.Email,
                                                     credencialesUsuario.Password,
                                                     isPersistent: false,
                                                     lockoutOnFailure: false)).Succeeded ?
              await ConstruirToken(credencialesUsuario) : BadRequest("Login incorrecto");

        [HttpGet("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<RespuestaAutenticacion>> Renovar()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var credencialesUsuario = new CredencialesUsuario()
            {
                Email = email,

            };

            return await ConstruirToken(credencialesUsuario);
        }

        //Generar Hash y Token
        private async Task<RespuestaAutenticacion> ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuario.Email),
            };
            var obtenerRol = "";
            var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);
            var existingUserRole = _context.Set<IdentityUserRole<string>>().FirstOrDefault(ur => ur.UserId == usuario.Id);
            
            if(existingUserRole != null)
            {
                obtenerRol = _roleManager.Roles.Where(x => x.Id == existingUserRole.RoleId).FirstOrDefault().Name;
            }
            else
            {
                obtenerRol = "Sin Rol";

            }

            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddYears(1);

            var securtityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securtityToken),
                Expiracion = expiracion,
                FirstName = usuario.FirstName,
                LastName = usuario.LastName ,
                RolName = obtenerRol,
                UserName= usuario.UserName,
            };
        }

        [HttpPost("AsignarRolUsuario")]
        public async Task<ActionResult> HacerAdmin(EditarAdminDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);
            if (usuario != null)
            {

                var existingClaims = await userManager.GetClaimsAsync(usuario);
                var claimExists = existingClaims.Any(c =>  c.Type == editarAdminDTO.TipoRol && c.Value == editarAdminDTO.TipoRol.ToLower() && c.Issuer == usuario.Id);


                if (!claimExists)
                {
                    await userManager.AddClaimAsync(usuario, new Claim(editarAdminDTO.TipoRol.ToUpper(), editarAdminDTO.TipoRol.ToUpper()));
                }
                else
                {
                    var existingClaim = existingClaims.FirstOrDefault(c => c.Type == editarAdminDTO.TipoRol.ToUpper() && c.Value == editarAdminDTO.TipoRol.ToUpper() && c.Issuer == usuario.Id);

                    var removeResult = await userManager.RemoveClaimAsync(usuario, existingClaim);

                    await userManager.AddClaimAsync(usuario, new Claim(editarAdminDTO.TipoRol, editarAdminDTO.TipoRol.ToLower()));

                }

                var existingRole = await _roleManager.FindByNameAsync(editarAdminDTO.TipoRol.ToUpper());
                if (existingRole == null)
                {
                    // Crear un nuevo rol si no existe
                    var newRole = new IdentityRole { Name = editarAdminDTO.TipoRol.ToUpper() };
                    var result = await _roleManager.CreateAsync(newRole);
                    if (result.Succeeded)
                    {
                        var Roles = await _roleManager.FindByNameAsync(editarAdminDTO.TipoRol.ToUpper());
                        var claim = new Claim(editarAdminDTO.TipoRol, editarAdminDTO.TipoRol.ToUpper(), ClaimValueTypes.String, usuario.Id);

                   var resultClaimRol =      await _roleManager.AddClaimAsync(Roles, claim);


                        if (resultClaimRol.Succeeded)
                        {
                            var userRole = new IdentityUserRole<string>
                            {
                                UserId = usuario.Id,
                                RoleId = Roles.Id
                            };
                            _context.Set<IdentityUserRole<string>>().Add(userRole);
                            await _context.SaveChangesAsync();
                            return Ok("Asignacion Exitosa");
                        }
                        else
                        {
                            return BadRequest("Error al momento de crear los roles  claim");
                        }

                    }
                    else
                    {
                        return BadRequest("El usuario no exite");
                    }
                }
                else
                {
                    return BadRequest("El usuario :  "+usuario.UserName +" ya tiene el rol : "+editarAdminDTO.TipoRol.ToUpper());
                }




                return Ok("Asignacion Exitosa");
            }
            else
            {
                return BadRequest("No se encontro usuario registrado");
            }

        }



        [HttpPost("RemoverRolUsuarios")]
        public async Task<ActionResult> RemoverAdmin(EditarAdminDTO editarAdminDTO)
        {
            var obtenerValueClaim = HttpContext.User.Claims.Where(claim => claim.Type == editarAdminDTO.TipoRol).FirstOrDefault();
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);
            await userManager.RemoveClaimAsync(usuario, new Claim(obtenerValueClaim.Type, obtenerValueClaim.Value));
            return NoContent();
        }

        [HttpGet("ValidateToken")]
        public async Task<ActionResult> ValidateTokenUser(string token)
        {
            try
            {
                var validateToken = await ValidateToken(token);
                if (validateToken is null)
                {
                    return BadRequest("Token no valido");
                }
                return Ok(validateToken);
            }
            catch (Exception)
            {
                return BadRequest("Token no valido");
            }
        }

        private Task<string> ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["llavejwt"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "email").Value;

                // return user id from JWT token if validation successful
                return Task.FromResult(userId);
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword(CredencialesUsuario credencialesUsuario)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            if (user == null)
            {
                return NotFound();
            }
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, credencialesUsuario.Password);
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Ocurrio un error al cmabiar su contraseña");
                //throw exception......
            }
            return Ok("Contraseña actualizada con exito!");
        }
    }
}