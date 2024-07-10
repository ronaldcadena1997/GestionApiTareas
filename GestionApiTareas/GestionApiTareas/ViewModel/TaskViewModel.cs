namespace GestionApiTareas.ViewModel
{
    public class TaskViewModel
    {
        public string idEstuduante { get; set; }
        public string nombre { get; set; }

        public long nota { get; set; }
        public string descripcion { get; set; }

        public bool estadoNota { get; set; }    

        public long idTask { get; set; }


    }


    public class ItemUsuarioTarea
    {
        public long idTask { get; set; }
        public string tarea { get; set; }
        public string usuario { get; set; }


        public string usuarioId { get; set; }

    }


    public class RegisterTaskViewModel
    {
        public string idEstuduante { get; set; }
        public string nombre { get; set; }

        public long nota { get; set; }

        public string descripcion { get; set; }

        public long idTask { get; set; }


    }
}
