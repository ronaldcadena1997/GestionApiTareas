using System.ComponentModel.DataAnnotations;

namespace GestionApiTareas.Entities
{
    public class CrudEntities
    {
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }

        [MaxLength(350)]
        public string SesionRegistro { get; set; }

        [MaxLength(350)]
        public string SesionModificacion { get; set; }

        [MaxLength(350)]
        public string SesionEliminacion { get; set; }

        [MaxLength(350)]
        public string IpRegistro { get; set; }

        [MaxLength(350)]
        public string IpModificacion { get; set; }

        [MaxLength(350)]
        public string IpEliminacion { get; set; }

        [MaxLength(350)]
        public string UsuarioRegistro { get; set; }

        [MaxLength(350)]
        public string UsuarioModificacion { get; set; }

        [MaxLength(350)]
        public string UsuarioEliminacion { get; set; }

        [MaxLength(350)]
        public string SistemaEliminacion { get; set; }

        [MaxLength(350)]
        public string SistemaRegistro { get; set; }

        [MaxLength(350)]
        public string SistemaModificacion { get; set; }
    }
}