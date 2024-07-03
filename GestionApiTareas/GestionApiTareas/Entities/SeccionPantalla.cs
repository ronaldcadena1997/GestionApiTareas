using System.ComponentModel.DataAnnotations;

namespace GestionApiTareas.Entities
{
    public class SeccionPantalla : CrudEntities
    {
        [Key]
        public long  idSeccionPantalla { get; set; }


        [MaxLength(500)]
        public string NombreSeccionPantalla { get; set; }

        [MaxLength(500)]
        public string UrlPantalla { get; set; }

        public int? posicionSeccionPantalla { get; set; }

    }
}
