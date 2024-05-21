using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;



namespace brouillon_projet
{

    public class MariaDB
    {
        public void testConnection()
        {
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=Mococ22112003*; database=uno_game;";
            try
            {

                MySqlConnection conn = new MySqlConnection(myConnectionString);
                conn.Open();
                var stm = "SELECT VERSION()";
                var cmd = new MySqlCommand(stm, conn);
                 
                var version = cmd.ExecuteScalar().ToString();
                Console.WriteLine($"MariaDB version: {version}"); 
            }
            catch (Exception ex) { }
        }
        public void createDB()
        {

            string connStr = "server=localhost;uid=root;pwd=Mococ22112003*; database=uno_game;";
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "CREATE DATABASE IF NOT EXISTS uno_game;";
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");
            }
        }

        public void tryCreateAlterTable()
        {
            string connStr = "server=localhost;uid=root;pwd=Mococ22112003*; database=uno_game;";
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();

                cmd.CommandText = "CREATE TABLE card (cid INT AUTO_INCREMENT PRIMARY KEY, number INTEGER, color VARCHAR(25))";
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");

                cmd.CommandText = "ALTER TABLE card ADD COLUMN type VARCHAR(25)";
                response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");

                cmd.CommandText = "INSERT INTO card (number, color, type) VALUES (@number, @color, @type)";
                cmd.Parameters.AddWithValue("@number", 5); // Exemple de nombre
                cmd.Parameters.AddWithValue("@color", "Red"); // Exemple de couleur
                cmd.Parameters.AddWithValue("@type", "Number"); // Exemple de type
                response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response (Insert into card): {response}");
            }
        }

        public void createUser(string username, string password)
        {
            string connStr = "server=localhost;uid=root;pwd=Mococ22112003*; database=uno_game;";
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO users (username, password) VALUES (@username, @password)";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public bool Login(string username, string password)
        {
            string connStr = "server=localhost;uid=root;pwd=Mococ22112003*; database=uno_game;";
            using (var conn = new MySqlConnection(connStr))
            try
            {
                
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dbPassword = reader["password"].ToString();
                                if (dbPassword == password)
                                {
                                    // Mettre à jour l'état connecté de l'utilisateur
                                    reader.Close();
                                    var updateCmd = new MySqlCommand("UPDATE users SET connected = TRUE WHERE username = @username", conn);
                                    updateCmd.Parameters.AddWithValue("@username", username);
                                    updateCmd.ExecuteNonQuery();
                                    return true;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect password.");
                                    return false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("User not found. Creating a new user...");
                                createUser(username, password); // Crée un nouvel utilisateur
                                return true; // Connexion réussie pour le nouvel utilisateur créé
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
            }
            return false;
        }

        public void CreateOrRetrieveUser(string username, string password)
        {
            // Vérifier si l'utilisateur existe déjà dans la base de données
            string connStr = "server=localhost;uid=root;pwd=Mococ22112003*;database=uno_game;";
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM users WHERE username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        // L'utilisateur n'existe pas, le créer
                        createUser(username, password);
                    }
                    else
                    {
                        Console.WriteLine($"User '{username}' already exists.");
                    }
                }
            }
        }

        public void ShowUserScores(string username) // corrected method
        {
            string connStr = "server=localhost;uid=root;pwd=Mococ22112003*; database=uno_game;";
            using (var conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    var cmd = new MySqlCommand("SELECT MAX(score) AS max_score, play_date FROM plays WHERE username = @username GROUP BY play_date ORDER BY max_score DESC LIMIT 1", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"Username: {username}, Max Score: {reader["max_score"]}, Date: {reader["play_date"]}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ShowUserScores error: {ex.Message}");
                }
            }
        }
        public void ListTable()

        {
            string connStr = "server=localhost;uid=root;pwd=Mococ22112003*; database=uno_game;";
            var connection = new MySqlConnection(connStr);

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT username FROM users", connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // access your record colums by using reader
                        Console.WriteLine(reader["username"]);
                    }
                }
            }
            catch (Exception ex)
            {
                // handle exception here
            }
            finally
            {
                connection.Close();
            }

        }
        public void AddScore(string username, int score)

        {
            string connStr = "server=localhost;uid=root;pwd=Mococ22112003*; database=uno_game;";
            var connection = new MySqlConnection(connStr);
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    var cmd = new MySqlCommand("INSERT INTO plays (username, play_date, score) VALUES (@username, @play_date, @score)", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@play_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddScore error: {ex.Message}");
            }
        }



        public void deletePlayer(string username)
        {
            string connStr = "server=localhost;uid=root;pwd=Mococ22112003*; database=uno_game;";
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "DELETE FROM users WHERE username = @username";
                cmd.Parameters.AddWithValue("@username", username);
                var response = cmd.ExecuteNonQuery();
                Console.WriteLine($"Response: {response}");

            }

        }

        public void userExistence (string username)
        {
            string connStr = "server=localhost;uid=root;pwd=Mococ22112003*; database=uno_game;";
            using (var conn = new MySqlConnection(connStr))
            using (var cmd = conn.CreateCommand())
                try
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("SELECT username FROM users where username = = @username", conn);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine(reader["username"]);
                            reader.Close();

                            command.CommandText = "UPDATE users SET connected = 1 WHERE username = @username";
                            var response = command.ExecuteNonQuery();
                            Console.WriteLine($"Response: {response}");
                        }
                    }
                }

                catch (Exception)
                {
                   
                    Console.WriteLine("user does not exist or incorrect password");
                    
                }
                finally
                {
                    conn.Close();
                }

        }


    }

}


