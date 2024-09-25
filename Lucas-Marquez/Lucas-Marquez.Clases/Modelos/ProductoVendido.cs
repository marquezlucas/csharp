namespace Lucas_Marquez.Clases.Modelos
{
    public class ProductoVendido
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public ProductoVendido(int id, int idProducto, int cantidad, decimal precioVenta)
        {
            Id = id;
            IdProducto = idProducto;
            Cantidad = cantidad;
            PrecioVenta = precioVenta;
        }
    }
}


/*namespace Lucas_Marquez.Clases
{
    public class ProductoVendido
    {
        private int _id;
        private int _idProducto;
        private int _stock;
        private int _idVenta;

        public ProductoVendido(Producto producto, Venta venta)
        {
            _id = 0;
            _idProducto = producto.GetId();
            _stock = 0;
            _idVenta = venta.GetId();
        }
    }
}*/
