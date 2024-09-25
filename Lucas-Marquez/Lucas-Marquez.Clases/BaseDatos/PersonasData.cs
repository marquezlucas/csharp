using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Lucas_Marquez.Clases.Modelos;

namespace Lucas_Marquez.Clases.BaseDatos
{
    public static class PersonasData
    {
        private static readonly string _connectionString;

        static PersonasData()
        {
            string password = Environment.GetEnvironmentVariable("SQL_DB_PASSWORD");
            _connectionString = $"Data Source=(localdb)\\server1511;Initial Catalog=Coderhouse;User ID=sa;Password={password};TrustServerCertificate=True";
        }

        public static List<Personas> ListarPersonas()
        {
            DataSet dataSet = new DataSet();
            List<Personas> personas = new List<Personas>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Personas";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dataSet, "Personas");

                connection.Close();
            }

            foreach (DataRow row in dataSet.Tables["Personas"].Rows)
            {
                personas.Add(new Personas
                {
                    Id = (int)row["Id"],
                    Nombre = (string)row["Nombre"],
                    Password = (string)row["Password"],
                    FechaNacimiento = (DateTime)row["FechaNacimiento"]
                });
            }
            return personas;
        }

        public static Personas? ObtenerPersona(int id)
        {
            DataSet dataSet = new DataSet();
            string query = "SELECT * FROM Personas WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@Id", id);

                connection.Open();
                adapter.Fill(dataSet, "Personas");
                connection.Close();
            }

            if (dataSet.Tables["Personas"].Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dataSet.Tables["Personas"].Rows[0];
            return new Personas
            {
                Id = (int)row["Id"],
                Nombre = (string)row["Nombre"],
                Password = (string)row["Password"],
                FechaNacimiento = (DateTime)row["FechaNacimiento"]
            };
        }

        public static int CrearPersona(Personas persona)
        {
            string insertQuery = @"INSERT INTO Personas (Nombre, Password, FechaNacimiento)
                                   VALUES (@Nombre, @Password, @FechaNacimiento); 
                                   SELECT SCOPE_IDENTITY();";
            int id;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                command.Parameters.AddWithValue("@Password", persona.Password);
                command.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);

                connection.Open();
                id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return id;
        }

        public static void ModificarPersona(int id, Personas persona)
        {
            string updateQuery = @"UPDATE Personas
                                   SET Nombre = @Nombre, Password = @Password, FechaNacimiento = @FechaNacimiento
                                   WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                command.Parameters.AddWithValue("@Password", persona.Password);
                command.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void EliminarPersona(int id)
        {
            string deleteQuery = @"DELETE FROM Personas WHERE Id = @Id";
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


