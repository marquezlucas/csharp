using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Lucas_Marquez.Clases.Modelos;

namespace Lucas_Marquez.Clases.BaseDatos
{
    public static class ProductosData
    {
        private static readonly string _connectionString;

        static ProductosData()
        {
            string password = Environment.GetEnvironmentVariable("SQL_DB_PASSWORD");
            _connectionString = $"Data Source=(localdb)\\server1511;Initial Catalog=Coderhouse;User ID=sa;Password={password};TrustServerCertificate=True";
        }

        public static List<Productos> ListarProductos()
        {
            DataSet dataSet = new DataSet();
            List<Productos> productos = new List<Productos>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Productos";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dataSet, "Productos");

                connection.Close();
            }

            foreach (DataRow row in dataSet.Tables["Productos"].Rows)
            {
                productos.Add(new Productos
                {
                    Id = (int)row["Id"],
                    Descripcion = (string)row["Descripcion"],
                    Costo = (decimal)row["Costo"],
                    PrecioVenta = (decimal)row["PrecioVenta"],
                    Categoria = (string)row["Categoria"]
                });
            }
            return productos;
        }

        public static Productos? ObtenerProductos(int id)
        {
            DataSet dataSet = new DataSet();
            string query = "SELECT * FROM Productos WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@Id", id);

                connection.Open();
                adapter.Fill(dataSet, "Productos");
                connection.Close();
            }

            if (dataSet.Tables["Productos"].Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dataSet.Tables["Productos"].Rows[0];
            return new Productos
            {
                Id = (int)row["Id"],
                Descripcion = (string)row["Descripcion"],
                Costo = (decimal)row["Costo"],
                PrecioVenta = (decimal)row["PrecioVenta"],
                Categoria = (string)row["Categoria"]
            };
        }

        public static int CrearProductos(Productos producto)
        {
            string insertQuery = @"INSERT INTO Productos (Descripcion, Costo, PrecioVenta, Categoria)
                                   VALUES (@Descripcion, @Costo, @PrecioVenta, @Categoria); 
                                   SELECT SCOPE_IDENTITY();";
            int id;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Costo", producto.Costo);
                command.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                command.Parameters.AddWithValue("@Categoria", producto.Categoria);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return id;
        }

        public static void ModificarProducto(int id, Productos producto)
        {
            string updateQuery = @"UPDATE Productos
                                   SET Descripcion = @Descripcion, Costo = @Costo, 
                                       PrecioVenta = @PrecioVenta, Categoria = @Categoria
                                   WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Costo", producto.Costo);
                command.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                command.Parameters.AddWithValue("@Categoria", producto.Categoria);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void EliminarProducto(int id)
        {
            string deleteQuery = @"DELETE FROM Productos WHERE Id = @Id";
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

