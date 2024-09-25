using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Lucas_Marquez.Clases.Modelos;

namespace Lucas_Marquez.Clases.BaseDatos
{
    public static class ProductoVendidoData
    {
        private static readonly string _connectionString;

        static ProductoVendidoData()
        {
            string password = Environment.GetEnvironmentVariable("SQL_DB_PASSWORD");
            _connectionString = $"Data Source=(localdb)\\server1511;Initial Catalog=Coderhouse;User ID=sa;Password={password};TrustServerCertificate=True";
        }

        public static List<ProductoVendido> ListarProductosVendidos()
        {
            DataSet dataSet = new DataSet();
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM ProductoVendido";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dataSet, "ProductoVendido");

                connection.Close();
            }

            foreach (DataRow row in dataSet.Tables["ProductoVendido"].Rows)
            {
                productosVendidos.Add(new ProductoVendido
                {
                    Id = (int)row["Id"],
                    IdProducto = (int)row["IdProducto"],
                    Cantidad = (int)row["Cantidad"],
                    PrecioVenta = (decimal)row["PrecioVenta"]
                });
            }
            return productosVendidos;
        }

        public static ProductoVendido? ObtenerProductoVendido(int id)
        {
            DataSet dataSet = new DataSet();
            string query = "SELECT * FROM ProductoVendido WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@Id", id);

                connection.Open();
                adapter.Fill(dataSet, "ProductoVendido");
                connection.Close();
            }

            if (dataSet.Tables["ProductoVendido"].Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dataSet.Tables["ProductoVendido"].Rows[0];
            return new ProductoVendido
            {
                Id = (int)row["Id"],
                IdProducto = (int)row["IdProducto"],
                Cantidad = (int)row["Cantidad"],
                PrecioVenta = (decimal)row["PrecioVenta"]
            };
        }

        public static int CrearProductoVendido(ProductoVendido productoVendido)
        {
            string insertQuery = @"INSERT INTO ProductoVendido (IdProducto, Cantidad, PrecioVenta)
                                   VALUES (@IdProducto, @Cantidad, @PrecioVenta); 
                                   SELECT SCOPE_IDENTITY();";
            int id;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@IdProducto", productoVendido.IdProducto);
                command.Parameters.AddWithValue("@Cantidad", productoVendido.Cantidad);
                command.Parameters.AddWithValue("@PrecioVenta", productoVendido.PrecioVenta);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return id;
        }

        public static void ModificarProductoVendido(int id, ProductoVendido productoVendido)
        {
            string updateQuery = @"UPDATE ProductoVendido
                                   SET IdProducto = @IdProducto, Cantidad = @Cantidad, PrecioVenta = @PrecioVenta
                                   WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@IdProducto", productoVendido.IdProducto);
                command.Parameters.AddWithValue("@Cantidad", productoVendido.Cantidad);
                command.Parameters.AddWithValue("@PrecioVenta", productoVendido.PrecioVenta);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void EliminarProductoVendido(int id)
        {
            string deleteQuery = @"DELETE FROM ProductoVendido WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

