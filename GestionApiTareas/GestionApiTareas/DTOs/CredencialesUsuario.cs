using System.ComponentModel.DataAnnotations;

namespace GestionApiTareas.DTOs
{
    public class CredencialesUsuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FirstName { get; set; }

     
        public string LastName { get; set; }


        [Required]
        public string Password { get; set; }
    }
}