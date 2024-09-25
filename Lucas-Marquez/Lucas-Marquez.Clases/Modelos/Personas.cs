
namespace Lucas_Marquez.Clases.Modelos
{
    public class Personas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public Personas(int id, string nombre, string password, DateTime fechaNacimiento)
        {
            Id = id;
            Nombre = nombre;
            Password = password;
            FechaNacimiento = fechaNacimiento;
        }
    }
}


/*namespace Lucas_Marquez.Clases
{
    public class Usuario
    {
        private int _id;
        private string _nombre;
        private string _apellido;
        private string _nombreUsuario;
        private string _contraseña;
        private string _mail;

        public Usuario() { 
            _id = 0;
            _nombre = string.Empty;
            _apellido = string.Empty;
            _nombreUsuario = string.Empty;
            _contraseña = string.Empty;
            _mail = string.Empty;
        }

        public int GetId()
        { 
            return _id; 
        }
    }
}*/
