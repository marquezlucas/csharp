namespace Lucas_Marquez.Clases.Modelos
{
    public class Productos
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Categoria { get; set; }

        public Productos(int id, string descripcion, decimal costo, decimal precioVenta, string categoria)
        {
            Id = id;
            Descripcion = descripcion;
            Costo = costo;
            PrecioVenta = precioVenta;
            Categoria = categoria;
        }
    }
}


/*namespace Lucas_Marquez.Clases
{
    public class Producto
    {
        private int _id;
        private string _descripcion;
        private double _costo;
        private double _precioVenta;
        private int _stock;
        private int _idUsuario;

        public Producto(Usuario usuario)
        {
            _id = 0;
            _descripcion = string.Empty;
            _costo = 0;
            _precioVenta = 0;
            _stock = 0;
            _idUsuario = usuario.GetId();
        }
        public int GetId()
        {
            return _id;
        }
    }
}*/