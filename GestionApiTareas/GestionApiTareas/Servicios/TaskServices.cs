using GestionApiTareas.Entities;
using GestionApiTareas.ViewModel;
using System.Data.Entity;

namespace GestionApiTareas.Servicios
{
    public class TaskServices
    {
        private static string _usuario;
        private static string _ip;
        private readonly ApplicationDbContext context;

        public TaskServices(string usuario, string ip, ApplicationDbContext context)
        {
            _ip = ip;
            _usuario = usuario;
            this.context = context;
        }


        public List<TaskViewModel> GetAllTask() => context.TaskEstudiantes
.Where(x => x.Activo)
.Select(x => new TaskViewModel
{
    idTask = x.idTask,
    nota = x.nota,
   estadoNota = x.estadoNota,
    descripcion = x.descripcion,
    idEstuduante = x.idEstudiante,
    nombre = x.nombreEstuiante
})
.ToList();


        public bool EstadoSubidaTask(long idTask)
        {
            using (var contexto = new ApplicationDbContext())
            {
                var TaskEncontradosD = contexto.TaskEstudiantes.Where(x => x.Activo && x.idTask == idTask).FirstOrDefault();
                if (TaskEncontradosD == null)
                    return false;
           
                TaskEncontradosD.estadoNota =true;
                TaskEncontradosD.FechaModificacion = DateTime.Now;
                TaskEncontradosD.UsuarioModificacion = _usuario;
                TaskEncontradosD.IpModificacion = _ip;
                TaskEncontradosD.SistemaModificacion = "Sistema de Tareas";
                contexto.SaveChanges();
                return true;
            }
        }

        public bool DeleteTask(long idTask)
        {
            using (var contexto = new ApplicationDbContext())
            {
                var TaskEncontradosD = contexto.TaskEstudiantes.Where(x => x.Activo && x.idTask == idTask).FirstOrDefault();
                if (TaskEncontradosD == null)
                    return false;
                TaskEncontradosD.Activo = false;
                TaskEncontradosD.FechaEliminacion = DateTime.Now;
                TaskEncontradosD.UsuarioEliminacion = _usuario;
                TaskEncontradosD.IpEliminacion = _ip;
                TaskEncontradosD.SistemaEliminacion = "Sistema de Tareas";
                contexto.SaveChanges();
                return true;
            }
        }

        public async Task<bool> SaveTask(RegisterTaskViewModel tarea)
        {
            using (var contexto = new ApplicationDbContext())
            {
                long? idTaskData = null;
                if (tarea.idTask != 0)
                {
                    idTaskData = tarea.idTask;
                }
               
            
                var TaskEncontrados = contexto.TaskEstudiantes.Where(x => x.idTask == idTaskData).FirstOrDefault();
                if (TaskEncontrados == null)
                {
                    TaskEstudiantes NesTask = new TaskEstudiantes();
                    
                    NesTask.idEstudiante = tarea.idEstuduante;
                    NesTask.nombreEstuiante = contexto.Users.Where(x => x.Bloqueo != true && x.Id == tarea.idEstuduante).FirstOrDefault().FirstName;
                    NesTask.descripcion = tarea.descripcion ;
                    NesTask.Activo = true;
                    NesTask.nota = 0;
                    NesTask.FechaRegistro = DateTime.Now;
                    NesTask.UsuarioRegistro = _usuario;
                    NesTask.IpRegistro = _ip;
                    NesTask.SistemaRegistro = "Sistema de Tareas";
                    await contexto.TaskEstudiantes.AddAsync(NesTask);
                    await contexto.SaveChangesAsync();
                }
                else
                {
                    TaskEncontrados.idEstudiante = tarea.idEstuduante;
                    TaskEncontrados.nombreEstuiante = contexto.Users.Where(x => x.Bloqueo != true && x.Id == tarea.idEstuduante).FirstOrDefault().FirstName;
                    TaskEncontrados.nota = tarea.nota ;
                    TaskEncontrados.estadoNota = true;
                    TaskEncontrados.descripcion = tarea.descripcion;
                    TaskEncontrados.Activo = true;
                    TaskEncontrados.FechaModificacion = DateTime.Now;
                    TaskEncontrados.UsuarioModificacion = _usuario;
                    TaskEncontrados.IpModificacion = _ip;
                    TaskEncontrados.SistemaModificacion = "Sistema de Tareas";
                    await contexto.SaveChangesAsync();
                }
                return true;
            }
        }


    }
}
