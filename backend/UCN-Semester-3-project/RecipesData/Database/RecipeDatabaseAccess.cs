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
        public Recipe GetRecipeById(Guid id)
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
                        recipe = BuildRecipeObject(reader);
                    }
                    reader.Close();
                }
                GetIngredientsByRecipe(connection, recipe);
                GetInstructionsByRecipe(connection, recipe);
            }
            return recipe;
        }

        public List<Recipe> GetRecipes()
        {
            List<Recipe> foundRecipes;
            Recipe readRecipe;
            //
            string queryString = "SELECT recipeId FROM recipe";
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand readCommand = new SqlCommand(queryString, con))
            {
                con.Open();
                // Execute read
                SqlDataReader recipeReader = readCommand.ExecuteReader();
                // Collect data
                foundRecipes = new List<Recipe>();
                while (recipeReader.Read())
                {
                    readRecipe = GetRecipeById(Guid.Parse(recipeReader.GetString(recipeReader.GetOrdinal("recipeId"))));
                    foundRecipes.Add(readRecipe);
                }

                con.Close();
            }
            return foundRecipes;

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

        private void getInstructionsByRecipe(SqlConnection con, Recipe recipe)
        {
            using (SqlCommand instructionCommand = con.CreateCommand())
            {
                instructionCommand.CommandText = "select * from instruction where recipeId = @id";

                instructionCommand.Parameters.AddWithValue("@id", recipe.RecipeId);

                SqlDataReader reader = instructionCommand.ExecuteReader();
                while (reader.Read())
                {
                    Instruction instruction = BuildInstructionObject(reader);
                    recipe.Instructions.Add(instruction);
                }
                reader.Close();
            }
        }



        // private 

        private void GetIngredientsByRecipe(SqlConnection con, Recipe recipe)
        {
            using (SqlCommand command = con.CreateCommand())
            {
                command.CommandText = "select * from ingredient WHERE recipeId = @id";

                command.Parameters.AddWithValue("@id", recipe.RecipeId);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ingredient ingredient = BuildIngredientObject(reader);
                    recipe.Ingredients.Add(ingredient);
                }
                reader.Close();
            }
        }

        private void GetInstructionsByRecipe(SqlConnection con, Recipe recipe)
        {
            using (SqlCommand instructionCommand = con.CreateCommand())
            {
                instructionCommand.CommandText = "select * from instruction where recipeId = @id";

                instructionCommand.Parameters.AddWithValue("@id", recipe.RecipeId);

                SqlDataReader reader = instructionCommand.ExecuteReader();
                while (reader.Read())
                {
                    Instruction instruction = BuildInstructionObject(reader);
                    recipe.Instructions.Add(instruction);
                }
                reader.Close();
            }
        }
        private Recipe BuildRecipeObject(SqlDataReader reader)
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

        private Ingredient BuildIngredientObject(SqlDataReader reader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.name = reader.GetString(reader.GetOrdinal("name"));
            ingredient.amount = reader.GetInt32(reader.GetOrdinal("amount"));
            ingredient.unit = reader.GetString(reader.GetOrdinal("unit"));
            return ingredient;
        }

        private Instruction BuildInstructionObject(SqlDataReader reader)
        {
            Instruction instruction = new Instruction();
            instruction.Step = reader.GetInt32(reader.GetOrdinal("step"));
            instruction.Description = reader.GetString(reader.GetOrdinal("description"));
            return instruction;
        }

    }
}
