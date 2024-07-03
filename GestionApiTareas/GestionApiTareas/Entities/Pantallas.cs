using System.ComponentModel.DataAnnotations;

namespace GestionApiTareas.Entities
{
    public class Pantallas : CrudEntities
    {
        [Key]
        public long idPantalla { get; set; }

        [MaxLength(500)]
        public string NombrePantalla { get; set; }

        [MaxLength(500)]
        public string UrlPantalla { get; set; }

        [MaxLength(500)]
        public string IconoPantalla { get; set; }

        public int? PosicionPantalla { get; set; }
    }
}
