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
        public void saveUser(User user) {

            string queryString = "insert into [dbo].[user] (name,licensePlate,date) values (@name, @licensePlate,@date);";
            using (SqlConnection connection = new SqlConnection(
                      Program.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@licensePlate", user.LicensePlate);
                command.Parameters.AddWithValue("@date", user.Date);


                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void deleteUser(string name)
        {
            string queryString = "delete from [dbo].[user] where name=@name;";
            using (SqlConnection connection = new SqlConnection(
                      Program.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                command.Parameters.AddWithValue("@name", name);


                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<string> getNames()
        {
            List<string> names =  new List<string>();
            string queryString = "SELECT name FROM [dbo].[user];";
            using (SqlConnection connection = new SqlConnection(
                      Program.connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        names.Add(reader[0].ToString());
                    }
                    reader.Close();
                }
            }
            return names;
        }
        public string getLicensePlateFromName(string name)
        {
            string queryString = "SELECT licensePlate FROM [dbo].[user] where name=@name;";
            using (SqlConnection connection = new SqlConnection(
                      Program.connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                command.Parameters.AddWithValue("@name", name);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader[0].ToString();
                    }
                    reader.Close();
                }
            }
            return "";
        }



    }
}
