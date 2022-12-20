using Microsoft.Extensions.Configuration;
using RecipesData.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Database
{
    public class CountDataBaseAcces
    {
        private string _connectionString;

        public CountDataBaseAcces(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UcnConnection");
        }

        public int GetCount()
        {

            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM visitors";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = reader.GetInt32(reader.GetOrdinal("visit"));
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            return count;
        }

        public int AddCount()
        {

            int count = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();


                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "update visitors set visit=(select visit from visitors) + 1 ";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = reader.GetInt32(reader.GetOrdinal("visit"));
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            return count;
        }
    }
}
