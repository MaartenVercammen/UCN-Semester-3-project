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
    public class SwipedRecipeDatabaseAccess : ISwipedRecipeAccess
    {
        readonly string _connectionString;

        public SwipedRecipeDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UcnConnection");
        }

        public SwipedRecipeDatabaseAccess(string connetionstring)
        {
            _connectionString = connetionstring;
        }

        public SwipedRecipe GetSRById(Guid id)
        {
            String guidString = id.ToString();
            SwipedRecipe sRecipe = new SwipedRecipe();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM SwipedRecipe WHERE recipeId = @recipeId";
                    command.Parameters.AddWithValue("@recipeId", guidString);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sRecipe = BuildObject(reader);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            return sRecipe;
        }

        public SwipedRecipe CreateSR(SwipedRecipe swipedRecipe)
        {
            SwipedRecipe sRecipe = new SwipedRecipe();
            string querySwipedRecipe = "insert into SwipedRecipe values (@UserId, @RecipeId, @IsLiked)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = querySwipedRecipe;
                    command.Parameters.AddWithValue("@UserId", swipedRecipe.UserId.ToString());
                    command.Parameters.AddWithValue("@RecipeId", swipedRecipe.RecipeId.ToString());
                    command.Parameters.AddWithValue("@IsLiked", swipedRecipe.IsLiked);

                    command.ExecuteNonQuery();
                    sRecipe.UserId = swipedRecipe.UserId;
                    sRecipe.RecipeId = swipedRecipe.RecipeId;
                    sRecipe.IsLiked = swipedRecipe.IsLiked;
                }
                connection.Close();
            }
            return sRecipe;
        }

        public bool DeleteSR(Guid id)
        {
            bool removed = false;
            string guidString = id.ToString();

            string query = "delete from SwipedRecipe where recipeId = @id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                    removed = true;
                }
                connection.Close();
            }
            return removed;
        }

        // private functions
        private SwipedRecipe BuildObject(SqlDataReader reader)
        {
            SwipedRecipe swipedRecipe = new SwipedRecipe();
            swipedRecipe.UserId = Guid.Parse(reader.GetString(reader.GetOrdinal("userId")));
            swipedRecipe.RecipeId = Guid.Parse(reader.GetString(reader.GetOrdinal("recipeId")));
            swipedRecipe.IsLiked = reader.GetBoolean(reader.GetOrdinal("isLiked"));

            return swipedRecipe;
        }
    }
}
