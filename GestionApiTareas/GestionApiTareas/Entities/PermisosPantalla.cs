using System.ComponentModel.DataAnnotations;

namespace GestionApiTareas.Entities
{
    public class PermisosPantalla : CrudEntities
    {
        [Key]
        public long idPermisosPantalla { get; set; }
        public string idRol { get; set; }

        public long idPantalla { get; set; }

        public long idSeccionPantalla { get; set; }

        public int? posicionesPermisoPantalla { get; set; }

        public virtual Pantallas Pantalla { get; set; }

        public virtual SeccionPantalla SeccionPantalla { get; set; }

    }
}
