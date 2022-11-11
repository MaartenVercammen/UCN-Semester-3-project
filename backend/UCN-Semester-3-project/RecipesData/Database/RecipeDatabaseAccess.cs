using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipesData.Model;
using System.Data.SqlClient;

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
            Recipe recipe = new Recipe();
            try
            {

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM recipe WHERE recipeId = @id";
                        command.Parameters.AddWithValue("@id", id);

                        SqlDataReader reader = command.ExecuteReader();
                        recipe = BuildObject(reader);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error while getting recipe by id", e);
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
                        recipes = BuildObjects(reader);
                    }
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
            try
            {
                recipe.RecipeId = new Guid((byte[])reader.GetValue(reader.GetOrdinal("recipeId")));
                recipe.Name = reader.GetString(reader.GetOrdinal("recipeName"));
                recipe.Description = reader.GetString(reader.GetOrdinal("recipeDescription"));
                recipe.PictureURL = reader.GetString(reader.GetOrdinal("recipePictureUrl"));
                recipe.Time = reader.GetInt32(reader.GetOrdinal("time"));
                recipe.PortionNum = reader.GetInt32(reader.GetOrdinal("portionNum"));
            }
            catch (SqlException e)
            {
                throw new Exception("Error while mapping recipe", e);
            }
            return recipe;
        }

        private List<Recipe> BuildObjects(SqlDataReader reader)
        {
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                while (reader.Read())
                {
                    recipes.Add(BuildObject(reader));
                }
            }
            catch (SqlException e)
            {
                throw new Exception("Error while building objects for a list", e);
            }
            return recipes;
        }
    }
}
