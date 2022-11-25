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

        /// <summary>
        /// Retrieves a SwipedRecipe identified by a recipe's Guid
        /// </summary>
        /// <param name="id">The recipe's Guid</param>
        /// <returns>The SwipedRecipe with the given Guid</returns>
        public SwipedRecipe GetSwipedRecipeById(Guid id)
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

        /// <summary>
        /// Retrieves a list of SwipedRecipes identified by the user's Guid
        /// </summary>
        /// <param name="userId">The user's Guid</param>
        /// <returns>A list of the user's all SwipedRecipe</returns>
        public List<SwipedRecipe> GetSwipedRecipesByUser(Guid userId)
        {
            String guidString = userId.ToString();
            List<SwipedRecipe> sRecipeList = new List<SwipedRecipe>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM SwipedRecipe WHERE userId = @userId";
                    command.Parameters.AddWithValue("@userId", guidString);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sRecipeList.Add(BuildObject(reader));
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            return sRecipeList;
        }

        /// <summary>
        /// Retrives a list of recipes that the user liked
        /// </summary>
        /// <param name="userId">The user's Guid</param>
        /// <returns>A list of the user's all SwipedRecipes where isLiked is ture</returns>
        public List<SwipedRecipe> GetLikedByUser(Guid userId)
        {
            String guidString = userId.ToString();
            List<SwipedRecipe> sRecipeList = new List<SwipedRecipe>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM SwipedRecipe WHERE userId = @userId AND isLiked = 1";
                    command.Parameters.AddWithValue("@userId", guidString);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sRecipeList.Add(BuildObject(reader));
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            return sRecipeList;
        }

        /// <summary>
        /// Inserts a new SwipedRecipe in the database
        /// </summary>
        /// <param name="swipedRecipe">The SwipedRecipe that is inserted into the database</param>
        /// <returns>The instance that was inserted into the database</returns>
        public SwipedRecipe CreateSwipedRecipe(SwipedRecipe swipedRecipe)
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

        /// <summary>
        /// Removes a SwipedRecipe from the database
        /// </summary>
        /// <param name="id">The Guid of the Recipe that is associated with this SwipedRecipe</param>
        /// <returns>True if the removal was successful</returns>
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
        /// <summary>
        /// Instantiates a new SwipedRecipe object
        /// </summary>
        /// <param name="reader">The SqlDataReader that reads the data from the database</param>
        /// <returns>The instantiated SwipedRecipe object</returns>
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
