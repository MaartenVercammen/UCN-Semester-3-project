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

        public Guid CreateRecipe(Recipe recipe)
        {
            Guid outid = Guid.NewGuid();
            recipe.RecipeId = outid;

            string queryRecipe = "insert into recipe (recipeId, name, description, pictureURL, time, portionNum, authorId) output INSERTED.recipeId values (@id, @name, @description, @pictureURL, @time, @portionNum, @authorId)";
            string queryIngredient = "insert into ingredient(name, amount, unit, recipeId) values (@name, @amount, @unit, @recipeId)";
            string querystruction = "insert into instruction(step, description, recipeId)  values (@step, @description, @recipeId)";


            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                SqlTransaction transaction;

                transaction = conn.BeginTransaction();

                cmd.Connection = conn;
                cmd.Transaction = transaction;

                try
                {
                    //create recipe
                    cmd.CommandText = queryRecipe;
                    cmd.Parameters.AddWithValue("id", recipe.RecipeId.ToString());
                    cmd.Parameters.AddWithValue("name", recipe.Name);
                    cmd.Parameters.AddWithValue("description", recipe.Description);
                    cmd.Parameters.AddWithValue("pictureURL", recipe.PictureURL);
                    cmd.Parameters.AddWithValue("time", recipe.Time);
                    cmd.Parameters.AddWithValue("authorId", recipe.Author.UserId);
                    cmd.Parameters.AddWithValue("portionNum", recipe.PortionNum);
                    string outidstring = (string)cmd.ExecuteScalar();
                    outid = Guid.Parse(outidstring);

                    //create ingredients
                    for(int i = 0; i < recipe.Ingredients.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        Ingredient ingredient = recipe.Ingredients[i];
                        cmd.CommandText = queryIngredient;
                        cmd.Parameters.AddWithValue("name", ingredient.name);
                        cmd.Parameters.AddWithValue("amount", ingredient.amount);
                        cmd.Parameters.AddWithValue("unit", ingredient.unit);
                        cmd.Parameters.AddWithValue("recipeId", recipe.RecipeId.ToString());
                        cmd.ExecuteNonQuery();
                    }

                    //create inestructions
                    for (int i = 0; i < recipe.Instructions.Count; i++)
                    {
                        cmd.Parameters.Clear();
                        Instruction instruction = recipe.Instructions[i];
                        cmd.CommandText = querystruction;
                        cmd.Parameters.AddWithValue("step", instruction.Step);
                        cmd.Parameters.AddWithValue("description", instruction.Description);
                        cmd.Parameters.AddWithValue("recipeId", recipe.RecipeId.ToString());
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                }
                catch(Exception)
                {
                    transaction.Rollback();
                }
                
            }
            return outid;
        }

        public List<Guid> GetNotSwipedGuidsByUserId(Guid userId){
            List<Guid> guids = new List<Guid>();
            string query = "select * from recipe where recipe.recipeId not in( select recipeId from swipedRecipe where userid = @id)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("id", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read()){
                    Guid id = Guid.Parse(reader.GetString(reader.GetOrdinal("recipeId")));
                    guids.Add(id);
                }

            }
            return guids;
        }

        bool IRecipeAccess.UpdateRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        bool IRecipeAccess.DeleteRecipe(Guid id)
        {
            bool deleteSuccesFull = false;
            using(SqlCommand cmd = new SqlCommand()){
                cmd.CommandText = "DELETE FROM recipe WHERE recipeId = @id";
                cmd.Parameters.AddWithValue("id", id);
                
                int rowsAffected = cmd.ExecuteNonQuery();
                deleteSuccesFull = rowsAffected > 0;
            } 
            return deleteSuccesFull; 
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
