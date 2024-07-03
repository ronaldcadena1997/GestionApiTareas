using System.ComponentModel.DataAnnotations;

namespace GestionApiTareas.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string TipoRol { get; set; }

      
    }
}