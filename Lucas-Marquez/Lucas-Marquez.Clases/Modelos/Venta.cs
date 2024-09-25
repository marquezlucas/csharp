namespace Lucas_Marquez.Clases.Modelos
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }
        public int IdPersonas { get; set; }
        public decimal MontoTotal { get; set; }
        public Venta(int id, string comentarios, int idPersonas, decimal montoTotal)
        {
            Id = id;
            Comentarios = comentarios;
            IdPersonas = idPersonas;
            MontoTotal = montoTotal;
        }
    }

}


/*namespace Lucas_Marquez.Clases
{
    public class Venta
    {
        private int _id;
        private string _comentarios;
        private int _idUsuario;

        public Venta(Usuario usuario)
        {
            _id = 0;
            _comentarios = string.Empty;
            _idUsuario = usuario.GetId();
        }
        public int GetId()
        {
            return _id;
        }
    }
}*/