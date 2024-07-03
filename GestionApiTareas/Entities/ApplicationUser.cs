using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GestionApiTareas.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public bool Bloqueo { get; set; }

        public long? IdRole { get; set; }

        public virtual RolesUsers RolesUsuarios { get; set; }


    }



}