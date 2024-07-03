using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GestionApiTareas.Entities
{
    public class RolesUsers :  CrudEntities
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(350)]
        public string RoleName { get; set; }
        


    }
}
