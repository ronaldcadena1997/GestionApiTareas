namespace GestionApiTareas.ViewModel
{
    public class UsuarioViewModel
    {
        public string nombre { get; set; }
        public string apellido { get; set; }

        public string correo { get; set; }

        

    }
    public class EliminarUsuario
    {

        public string id { get; set; }
    }

    public class UsuarioLista
    {
        public string id { get; set; }
     
        public string nombre { get; set; }
        public string apellido { get; set; }

        public string correo { get; set; }

        public string telefono { get; set; }

        public string contrasenia { get; set; }

        public string tipoRol { get; set; }

    }

    public class UsuarioRoles
    {

        public string idUser { get; set; }
        public string idRol { get; set; }

    }


    public class Usuario
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

    }

    public class RolesLista
    {

        public string id { get; set; }

        public string descripcion { get; set; }

    }
}
