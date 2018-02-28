using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BLL.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class AmigoRepository
    {
        SqlConnection connection;
        
        string strConString = @"Server=tcp:friendmanager.database.windows.net,1433;Initial Catalog=Amigo;Persist Security Info=False;User ID={your_username};Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public IEnumerable<Amigo> GetAllAmigos()
        {
            using (var connection = new SqlConnection(strConString))
            {
                var commandText = "SELECT * FROM pessoa";
                var selectCommand = new SqlCommand(commandText, connection);

                Amigo amigo = null;
                var amigos = new List<Amigo>();

                try
                {
                    connection.Open();

                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            amigo = new Amigo();
                            amigo.Id = (int)reader["id"];
                            amigo.Nome = reader["nome"].ToString();
                            amigo.SobreNome = reader["sobreNome"].ToString();
                            amigo.Email = reader["email"].ToString();
                            amigo.Nascimento = DateTime.Parse(reader["nascimento"].ToString());

                            amigos.Add(amigo);
                        }

                    }
                }
                finally
                {

                    connection.Close();
                }

                return amigos;
            }
        }

        public void AddAmigo(string Nome, string SobreNome, string Email, DateTime Nascimento)
        {
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "INSERT INTO pessoa (nome, sobreNome, email, nascimento) VALUES (@nome, @sobreNome, @email, @nascimento)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nome", Nome);
                cmd.Parameters.AddWithValue("@sobreNome", SobreNome);
                cmd.Parameters.AddWithValue("@email", Email);
                cmd.Parameters.AddWithValue("@nascimento", Nascimento);
                cmd.ExecuteNonQuery();
            }
        }

        public Amigo GetAmigo(int amigoId)
        {
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.pessoa WHERE id = @id", con);
                cmd.Parameters.AddWithValue("@id", amigoId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        return new Amigo()
                        {
                            Id = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            SobreNome = reader.GetString(2),
                            Email = reader.GetString(3),
                            Nascimento = reader.GetDateTime(4)
                        };
                }
            }
            return null;
        }

        public void UpdateAmigo(Amigo amigo)
        {
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "UPDATE dbo.pessoa SET nome = @nome, sobreNome = @sobreNome, email = @email, nascimento = @nascimento WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", amigo.Id);
                cmd.Parameters.AddWithValue("@nome", amigo.Nome);
                cmd.Parameters.AddWithValue("@sobreNome", amigo.SobreNome);
                cmd.Parameters.AddWithValue("@email", amigo.Email);
                cmd.Parameters.AddWithValue("@nascimento", amigo.Nascimento);
                cmd.ExecuteNonQuery();

            }
        }

        public void RemoveAmigo(int amigoId)
        {
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "DELETE FROM dbo.pessoa WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", amigoId);
                cmd.ExecuteNonQuery();
            }
        }

        //public void CreateAmigo(Amigo amigo)
        //{
        //    using (var connection = new SqlConnection(strConString))
        //    {
        //        var commandText = "INSERT INTO pessoa (nome, sobre_nome, email, data_aniversario) VALUES (@nome, @sobre_nome, @emial, @data_aniversario)";
        //        var insertCommand = new SqlCommand(commandText, connection);
        //        insertCommand.Parameters.AddWithValue("@nome", amigo.Nome);
        //        insertCommand.Parameters.AddWithValue("@sobre_nome", amigo.SobreNome);
        //        insertCommand.Parameters.AddWithValue("@email", amigo.Email);
        //        insertCommand.Parameters.AddWithValue("@data_aniversario", amigo.Nascimento);
        //        try
        //        {
        //            connection.Open();
        //            insertCommand.ExecuteNonQuery();
        //        }
        //        finally
        //        {

        //            connection.Close();
        //        }
        //    }
        //} 
    }
}
