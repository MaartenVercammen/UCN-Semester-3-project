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
    public class RecipeDatabaseAccess : IRecipeAccess
    {
        readonly string _connectionString;

        public RecipeDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UcnConnection");
        }

        public RecipeDatabaseAccess(string connetionstring)
        {
            _connectionString = connetionstring;
        }

        /// <summary>
        /// This method gets a recipe by it's id from the database
        /// </summary>
        Recipe IRecipeAccess.GetRecipeById(Guid id)
        {
            String guidString = id.ToString();
            Recipe recipe = new Recipe();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM recipe WHERE recipeId = @id";
                        command.Parameters.AddWithValue("@id", guidString);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            recipe = BuildObject(reader);
                        }
                        reader.Close();
                    }
                connection.Close();
                }
            
            return recipe;
        }
        /// <summary>
        /// This method gets all the recipes from the database
        /// </summary>
        List<Recipe> IRecipeAccess.GetRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM recipe";
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Recipe recipe = new Recipe();
                            recipe = BuildObject(reader);
                            recipes.Add(recipe);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error while getting recipes", e);
            }
            return recipes;
        }

        int IRecipeAccess.CreateRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        bool IRecipeAccess.UpdateRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        bool IRecipeAccess.DeleteRecipe(int id)
        {
            throw new NotImplementedException();
        }



        // private 
        private Recipe BuildObject(SqlDataReader reader)
        {
            Recipe recipe = new Recipe();
            recipe.RecipeId = Guid.Parse(reader.GetString(reader.GetOrdinal("recipeId")));
            recipe.Name = reader.GetString(reader.GetOrdinal("name"));
            recipe.Description = reader.GetString(reader.GetOrdinal("description"));
            recipe.PictureURL = reader.GetString(reader.GetOrdinal("pictureURL"));
            recipe.Time = reader.GetInt32(reader.GetOrdinal("time"));
            recipe.PortionNum = reader.GetInt32(reader.GetOrdinal("portionNum"));
            return recipe;
        }

    }
}
