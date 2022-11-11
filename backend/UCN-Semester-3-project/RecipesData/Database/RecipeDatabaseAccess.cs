using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipesData.Model;

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
        public override Recipe GetRecipeById(Guid id)
        {
            Recipe recipe = new Recipe();
            try
            {

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.commandText = "SELECT * FROM recipe WHERE recipeId = @id";
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
        public List<Recipe> GetRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.commandText = "SELECT * FROM recipe";

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

        public void CreateRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecipe(int id)
        {
            throw new NotImplementedException();
        }

        private Recipe BuildObject(SqlDataReader reader)
        {
            Recipe recipe = new Recipe();
            try
            {
                recipe.RecipeId = reader.GetGuid(reader.GetOrdinal("recipeId"));
                recipe.RecipeName = reader.GetString(reader.GetOrdinal("recipeName"));
                recipe.RecipeDescription = reader.GetString(reader.GetOrdinal("recipeDescription"));
                recipe.RecipePictureUrl = reader.GetString(reader.GetOrdinal("recipePictureUrl"));
                recipe.Time = reader.GetInt32(reader.GetOrdinal("time"));
                recipe.PortionNum = reader.GetInt32(reader.GetOrdinal("portionNum"));
            }
            catch (SqlException e)
            {
                throw new Exception("Error while mapping recipe", e);
            }
            return recipe;
        }

    }

    private List<Recipe> buildObjects(SqlDataReader reader)
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
