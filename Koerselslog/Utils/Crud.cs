﻿using System;
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
            
            string queryString = "insert into [dbo].[users] (name,licensePlate,date) values (@name, @licensePlate,@date);";
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

        public void updateUser(int id, string licensePlate)
        {
            string queryString = "update [dbo].[users] set licensePlate=@licensePlate where id=@id;";
            using (SqlConnection connection = new SqlConnection(
                      Program.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                command.Parameters.AddWithValue("@LicensePlate", licensePlate);
                command.Parameters.AddWithValue("@id", id);


                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void deleteUser(string name)
        {
            string queryString = "delete from [dbo].[users] where name=@name;";
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
            string queryString = "SELECT [name], [id] FROM [dbo].[users];";
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
                        names.Add(reader[0].ToString() + "|" + reader[1].ToString());
                    }
                    reader.Close();
                }
            }
            return names;
        }
        public string getLicensePlateFromId(int id)
        {
            string queryString = "SELECT licensePlate FROM [dbo].[users] where id=@id;";
            using (SqlConnection connection = new SqlConnection(
                      Program.connectionString))
            {
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                command.Parameters.AddWithValue("@id", id);
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

        public void createDrivingLog(int user_id, string assignment, string date)
        {
            string queryString = "insert into [dbo].[driving_logs] (assignment,date,user_id) values (@assignment, @date, @user_id);";
            using (SqlConnection connection = new SqlConnection(
                      Program.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    queryString, connection);
                command.Parameters.AddWithValue("@assignment", assignment);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@user_id", user_id);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }


    }
}