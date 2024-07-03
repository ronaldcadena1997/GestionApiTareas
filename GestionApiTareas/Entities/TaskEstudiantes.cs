using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GestionApiTareas.Entities
{
    public class TaskEstudiantes: CrudEntities
    {
        [Key]
        public long idTask { get; set; }

        [MaxLength(500)]
        public string idEstudiante { get; set; }


        [MaxLength(500)]
        public  string nombreEstuiante { get; set; }


        [MaxLength(500)]
        public string descripcion { get; set; }


    }
}
