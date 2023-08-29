using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Koerselslog
{
    internal class Crud
    {
        // Connection string for the database
        public string connectionString = @"Data Source=192.168.16.147,1433;Database=drivinglog;User Id=root;Password=Adm1n123;";
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Koerselslog
{
    internal class Crud
    {
        public string connectionString = @"Data Source=192.168.16.147,1433;Initial Catalog=drivinglog;User Id=root;Password=Adm1n123;";

        // Constructor: Initialize database and tables if they don't exist
        public Crud()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                // Create the database if it doesn't exist
                string createDbQuery = "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'drivinglog') CREATE DATABASE drivinglog;";
                using (SqlCommand command = new SqlCommand(createDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Create the users table if it doesn't exist
                string createUsersTableQuery = "USE drivinglog;" +
                                               "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'users') " +
                                               "CREATE TABLE users (" +
                                               "id INT PRIMARY KEY IDENTITY," +
                                               "name NVARCHAR(50) NOT NULL," +
                                               "licensePlate NVARCHAR(10) NOT NULL," +
                                               "date DATETIME NOT NULL," +
                                               "disabled BIT NOT NULL DEFAULT 0);";
                using (SqlCommand command = new SqlCommand(createUsersTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Create the driving_logs table if it doesn't exist
                string createDrivingLogsTableQuery = "USE drivinglog;" +
                                                     "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'driving_logs') " +
                                                     "CREATE TABLE driving_logs (" +
                                                     "id INT PRIMARY KEY IDENTITY," +
                                                     "user_id INT NOT NULL," +
                                                     "assignment NVARCHAR(100) NOT NULL," +
                                                     "distance INT NOT NULL," +
                                                     "date DATETIME NOT NULL," +
                                                     "disabled BIT NOT NULL DEFAULT 0," +
                                                     "FOREIGN KEY (user_id) REFERENCES users(id));";
                using (SqlCommand command = new SqlCommand(createDrivingLogsTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        // Save a new user to the database
        public void saveUser(User user)
        {
            string queryString = "insert into [dbo].[users] (name,licensePlate,date) values (@name, @licensePlate,@date);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@licensePlate", user.LicensePlate);
                command.Parameters.AddWithValue("@date", user.Date);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        // Update the license plate for an existing user
        public void updateUser(int id, string licensePlate)
        {
            string queryString = "update [dbo].[users] set licensePlate=@licensePlate where id=@id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@LicensePlate", licensePlate);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        // Disable a user by marking them as disabled
        public int deleteUser(int id)
        {
            string queryString = "update [dbo].[users] set disabled=@disabled where id=@id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@disabled", "true");
                command.Parameters.AddWithValue("@id", id);

                int rows = command.ExecuteNonQuery();
                connection.Close();
                return rows;
            }
        }

        // Disable a driving log by marking it as disabled
        public int deleteDrivingLog(int id)
        {
            string queryString = "update [dbo].[driving_logs] set disabled=@disabled where id=@id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@disabled", "true");
                command.Parameters.AddWithValue("@id", id);

                int rows = command.ExecuteNonQuery();
                connection.Close();
                return rows;
            }
        }

        // Get a list of names (user names and IDs) for ComboBox
        public List<string> getNames()
        {
            List<string> names = new List<string>();
            string queryString = "SELECT [name], [id] FROM [dbo].[users] where disabled=@disabled or disabled=@disabled2;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@disabled", "false");
                command.Parameters.AddWithValue("@disabled2", "0");
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        names.Add(reader[0].ToString() + "|" + reader[1].ToString());
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            return names;
        }

        // Get the license plate of a user based on ID
        public string getLicensePlateFromId(int id)
        {
            string queryString = "SELECT licensePlate FROM [dbo].[users] where id=@id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader[0].ToString();
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            return "";
        }

        // Create a new driving log entry
        public void createDrivingLog(int user_id, string assignment, int distance, string date)
        {
            string queryString = "insert into [dbo].[driving_logs] (assignment,distance,date,user_id) values (@assignment, @distance, @date, @user_id);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@assignment", assignment);
                command.Parameters.AddWithValue("@distance", distance);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@user_id", user_id);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        // Get details of driving log assignments
        public List<Utils.Assignments> drivingLogDetails()
        {
            List<Utils.Assignments> assignments = new List<Utils.Assignments>();
            string queryString = "SELECT [driving_logs].[id], [users].[name], [users].[licensePlate], [driving_logs].[assignment], [driving_logs].[date] FROM [dbo].[users], [dbo].[driving_logs] where [users].[id]=[driving_logs].[user_id];";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        assignments.Add(new Utils.Assignments(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString()));
                    }
                    reader.Close();
                    connection.Close();
                }
            }

            return assignments;
        }
    }
}
