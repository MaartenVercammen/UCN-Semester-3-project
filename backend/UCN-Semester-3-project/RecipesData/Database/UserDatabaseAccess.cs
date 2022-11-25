using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipesData.Model;
using System.Data.SqlClient;
using System.Data;

namespace RecipesData.Database
{
    public class UserDatabaseAccess : IUserAccess
    {
        readonly string _connectionString;

        public UserDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UcnConnection");
        }

        public UserDatabaseAccess(string connetionstring)
        {
            _connectionString = connetionstring;
        }

        public Guid CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Guid id)
        {
            string guidString = id.ToString();
            User user = new User();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select * from [user] where userId =@userId";
                    command.Parameters.AddWithValue("@userId", guidString);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = BuildObject(reader);
                        }
                        reader.Close();
                    }
                }
                return user;
            }
        }

        public List<User> GetUsers()
        {
            List<User> foundUsers;
            User readUser;

              string queryString = "SELECT userId FROM user";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader userReader = readCommand.ExecuteReader();
                // Collect data
                foundUsers = new List<User>();
                while (userReader.Read())
                {
                    readUser = GetUserById(Guid.Parse(userReader.GetString(userReader.GetOrdinal("userID"))));
                    foundUsers.Add(readUser);
                }

                con.Close();
            }
            return foundUsers;
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        // private
        private User BuildObject(SqlDataReader reader)
        {
            User user = new User();
            user.UserId = Guid.Parse(reader.GetString(reader.GetOrdinal("userId")));
            user.Email = reader.GetString(reader.GetOrdinal("email"));
            user.FirstName = reader.GetString(reader.GetOrdinal("firstName"));
            user.LastName = reader.GetString(reader.GetOrdinal("lastName"));
            user.Password = reader.GetString(reader.GetOrdinal("password"));
            user.Address = reader.GetString(reader.GetOrdinal("address"));
            return user;
        }
    }
}
