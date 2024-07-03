using WebAPIAutores.DTOs;

namespace GestionApiTareas.DTOs
{
    public class ColeccionDeRecursos<T> : Recurso where T : Recurso
    {
        public List<T> Valores { get; set; }
    }
}