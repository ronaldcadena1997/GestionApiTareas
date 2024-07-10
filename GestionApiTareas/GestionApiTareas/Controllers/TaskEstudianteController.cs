using GestionApiTareas.Entities;
using GestionApiTareas.Servicios;
using GestionApiTareas.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace GestionApiTareas.Controllers
{
    [ApiController]
    [Route("api/TaskEstudiantes/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TaskEstudianteController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IAuthorizationService authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        protected TaskServices _service;

        public TaskEstudianteController(IConfiguration configuration, IAuthorizationService Authorization, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            this.authorizationService = Authorization;
            this.configuration = configuration;
            this.userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
            this.context = context;
            string userName = Task.Run(async () => (await userManager.FindByEmailAsync(httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("email", StringComparison.CurrentCultureIgnoreCase))?.Value ?? ""))?.UserName ?? "Desconocido").Result;
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            this._service = new TaskServices(userName, ip, context);
        }

        

             [HttpGet("ListaTaskViewModelEstudiante")]
        public async Task<IActionResult> GetTaskEstudianteInfo(string idTask) => Ok(_service.ListaEstudianteInfo(idTask));

        [HttpGet("Listar")]
        public async Task<IActionResult> GetTaskEstudiante() => Ok( _service.GetAllTask());


        [HttpPost("Registrar")]
        public async Task<IActionResult> NuevaTaskList(RegisterTaskViewModel tarea) => Ok(await _service.SaveTask(tarea));

        

           [HttpPost("EstadosSubidas")]
        public async Task<IActionResult> EstadosSubidasD(long idTask) => Ok(_service.EstadoSubidaTask(idTask));


        [HttpPost("Deshabilitar")]
        public async Task<IActionResult> EliminaTask(long idTask) => Ok( _service.DeleteTask(idTask));
  
    
    }
}
