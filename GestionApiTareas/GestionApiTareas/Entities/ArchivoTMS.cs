using System.ComponentModel.DataAnnotations;


namespace GestionApiTareas.Entities
{
    public class ArchivoTMS
    {
        [Key]
        public long IdArchivo { get; set; }

        [MaxLength(500)]
        public string EnlacePublico { get; set; }
        [MaxLength(500)]
        public string EnlacePrivado { get; set; }
        [MaxLength(500)]
        public string NombreOriginal { get; set; }
        [MaxLength(500)]
        public string NombreSistema { get; set; }
        public string TipoArchivo { get; set; }


        [MaxLength(500)]
        public string Descripcion { get; set; }

        [MaxLength(500)]
        public string Tipo { get; set; }

        public string IdSync { get; set; }
        public long? ItemId { get; set; }

        public bool Lectura { get; set; }
        public bool Escritura { get; set; }
        public bool Descarga { get; set; }
        public bool Sistema { get; set; }
        public bool Disponible { get; set; }
        public decimal TamanioKb { get; set; }
        public decimal TamanioMb { get; set; }
        public bool Imagen { get; set; }
        [MaxLength(500)]
        public string Extension { get; set; }
        public decimal? Alto { get; set; }
        public decimal? Ancho { get; set; }
        public DateTime FechaCarga { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }

        public bool IsArchivoNovedad { get; set; }
        public long? PreguntaRutaId { get; set; }
        public long? ReservaId { get; set; }
        public long? ReservaDetalleId { get; set; }
        public long? ChoferId { get; set; }

    }
}
