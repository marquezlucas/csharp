using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Lucas_Marquez.Clases.Modelos;

namespace Lucas_Marquez.Clases.BaseDatos
{
    public static class VentaData
    {
        private static readonly string _connectionString;

        static VentaData()
        {
            string password = Environment.GetEnvironmentVariable("SQL_DB_PASSWORD");
            _connectionString = $"Data Source=(localdb)\\server1511;Initial Catalog=Coderhouse;User ID=sa;Password={password};TrustServerCertificate=True";
        }

        public static List<Venta> ListarVentas()
        {
            DataSet dataSet = new DataSet();
            List<Venta> ventas = new List<Venta>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Venta";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dataSet, "Venta");

                connection.Close();
            }

            foreach (DataRow row in dataSet.Tables["Venta"].Rows)
            {
                ventas.Add(new Venta
                {
                    Id = (int)row["Id"],
                    IdPersonas = (int)row["IdPersonas"],
                    Comentarios = (string)row["Comentarios"],
                    MontoTotal = (decimal)row["MontoTotal"]
                });
            }
            return ventas;
        }

        public static Venta? ObtenerVenta(int id)
        {
            DataSet dataSet = new DataSet();
            string query = "SELECT * FROM Venta WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@Id", id);

                connection.Open();
                adapter.Fill(dataSet, "Venta");
                connection.Close();
            }

            if (dataSet.Tables["Venta"].Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dataSet.Tables["Venta"].Rows[0];
            return new Venta
            {
                Id = (int)row["Id"],
                IdPersonas = (int)row["IdPersonas"],
                Comentarios = (string)row["Comentarios"],
                MontoTotal = (decimal)row["MontoTotal"]
            };
        }

        public static int CrearVenta(Venta venta)
        {
            string insertQuery = @"INSERT INTO Venta (IdPersonas, Comentarios, MontoTotal)
                                   VALUES (@IdPersonas, @Comentarios, @MontoTotal); 
                                   SELECT SCOPE_IDENTITY();";
            int id;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@IdPersonas", venta.IdPersonas);
                command.Parameters.AddWithValue("@Comentarios", venta.Comentarios);
                command.Parameters.AddWithValue("@MontoTotal", venta.MontoTotal);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return id;
        }

        public static void ModificarVenta(int id, Venta venta)
        {
            string updateQuery = @"UPDATE Venta
                                   SET IdPersonas = @IdPersonas, Comentarios = @Comentarios, MontoTotal = @MontoTotal
                                   WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@IdPersonas", venta.IdPersonas);
                command.Parameters.AddWithValue("@Comentarios", venta.Comentarios);
                command.Parameters.AddWithValue("@MontoTotal", venta.MontoTotal);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void EliminarVenta(int id)
        {
            string deleteQuery = @"DELETE FROM Venta WHERE Id = @Id";
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











