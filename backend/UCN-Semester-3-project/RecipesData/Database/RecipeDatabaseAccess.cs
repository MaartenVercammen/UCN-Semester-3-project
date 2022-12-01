using Microsoft.Extensions.Configuration;
using RecipesData.Model;
using System.Data.SqlClient;

namespace RecipesData.Database
{
    public class RecipeDatabaseAccess : IRecipeAccess
    {
        readonly string _connectionString;
        readonly UserDatabaseAccess _userDatabaseAccess;

        public RecipeDatabaseAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UcnConnection");
            _userDatabaseAccess = new UserDatabaseAccess(_connectionString);
        }

        public RecipeDatabaseAccess(string connetionstring)
        {
            _connectionString = connetionstring;
        }

        /// <summary>
        /// Retrieves a recipe identified by it's Guid.
        /// </summary>
        /// <param name="id">The recipe's Guid</param>
        /// <returns>The recipe with the given Guid</returns>
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

        /// <summary>
        /// Retrieves a list of all the Recipes in the database
        /// </summary>
        /// <returns>A list of recipes</returns>
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

        public List<Recipe> GetRandomRecipe(Guid userId)
        {
            List<Recipe> foundRecipes = new List<Recipe>();
            Recipe recipe = new Recipe();
            String userIdString = userId.ToString();            

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM recipe where recipeId not in (select recipeId from swipedRecipe where swipedRecipe.userId = @userId)";

                    command.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        recipe = BuildRecipeObject(reader);
                        foundRecipes.Add(recipe);
                    }
                    reader.Close();
                }
            }
            return foundRecipes;
        }

        public List<Recipe> GetRecipesSimplified() 
        {
            List<Recipe> foundRecipes;
            Recipe readRecipe;
            //
            string queryString = "SELECT recipeId, name, description, pictureUrl, time FROM recipe";
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
                    readRecipe = BuildRecipeSimplified(recipeReader);
                    foundRecipes.Add(readRecipe);
                    
                }

                con.Close();
            }
            return foundRecipes;
        }

        /// <summary>
        /// Inserts a recipe into the database
        /// </summary>
        /// <param name="recipe">The recipe that is inserted into the database</param>
        /// <returns>The Guid of the inserted recipe</returns>
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

                var transaction = conn.BeginTransaction();

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
                    string outIdString = (string)cmd.ExecuteScalar();
                    outid = Guid.Parse(outIdString);

                    //create ingredients
                    for (int i = 0; i < recipe.Ingredients.Count; i++)
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
                catch (Exception)
                {
                    transaction.Rollback();
                    outid = Guid.Empty;
                }

            }
            return outid;
        }

        /// <summary>
        /// Retrieves all the Guids from the database
        /// </summary>
        /// <returns>A list of Guids</returns>
        public List<Guid> GetNotSwipedGuidsByUserId(Guid userId)
        {
            List<Guid> guids = new List<Guid>();
            string query = "select recipeId from recipe where recipe.recipeId not in( select recipeId from swipedRecipe where userid = @id)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("id", userId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Guid id = Guid.Parse(reader.GetString(reader.GetOrdinal("recipeId")));
                    guids.Add(id);
                }

            }
            return guids;
        }


    public List<Recipe> GetLikedByUser(Guid userId)
    {
        List<Recipe> likedRecipes = new List<Recipe>();
        Recipe recipe = new Recipe();
        String userIdString = userId.ToString();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM recipe left JOIN [swipedRecipe] on recipe.recipeId = swipedRecipe.recipeId WHERE swipedRecipe.userId =@userId AND isLiked = 1";

                command.Parameters.AddWithValue("@userId", userIdString);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    recipe = BuildRecipeSimplified(reader);
                    likedRecipes.Add(recipe);
                }
                reader.Close();
            }
        }
        return likedRecipes;
    }
        
        /// <summary>
        /// Updates a recipe
        /// </summary>
        /// <param name="recipe">The recipe that is updated</param>
        /// <returns>True if the recipe was successfully updated</returns>

        
        public bool UpdateRecipe(Recipe recipe)
        {
            bool update = false;
            string queryRecipe = "update  [recipe] set name@name, description@description, pictureURL@pictureURL, time@time, portionNum@portionNum, authorId@authorId where recipeId = @idrecipeId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                
                    command.CommandText = queryRecipe;
                    command.Parameters.AddWithValue("id", recipe.RecipeId.ToString());
                    command.Parameters.AddWithValue("name", recipe.Name);
                    command.Parameters.AddWithValue("description", recipe.Description);
                    command.Parameters.AddWithValue("pictureURL", recipe.PictureURL);
                    command.Parameters.AddWithValue("time", recipe.Time);
                    command.Parameters.AddWithValue("authorId", recipe.Author.UserId);
                    command.Parameters.AddWithValue("portionNum", recipe.PortionNum);
                    //insturction
                    //ingredient

                    command.ExecuteNonQuery();
                    update = true;
                }
                connection.Close();
            }
            return update;
        }

        public bool DeleteRecipe(Guid id)
        {
            bool deleteSuccesFull = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.Transaction = conn.BeginTransaction();

                try
                {
                    //delete ingredient from recipe
                    cmd.CommandText = "DELETE FROM ingredient WHERE recipeId = @id";
                    cmd.Parameters.AddWithValue("id", id);

                    cmd.ExecuteNonQuery();

                    //delete instructions from recipe
                    cmd.CommandText = "DELETE FROM instruction WHERE recipeId = @id";

                    cmd.ExecuteNonQuery();

                    //delete recipe
                    cmd.CommandText = "DELETE FROM recipe WHERE recipeId = @id";

                    int rowsAffected = cmd.ExecuteNonQuery();
                    deleteSuccesFull = rowsAffected > 0;
                    cmd.Transaction.Commit();

                }
                catch (Exception) { cmd.Transaction.Rollback(); }


                conn.Close();
            }

            return deleteSuccesFull;
        }
        
        // private 
        /// <summary>
        /// Retrieves the ingredients of a recipe
        /// </summary>
        /// <param name="con">The Sqlconnection</param>
        /// <param name="recipe">The queried recipe</param>
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

        /// <summary>
        /// This is the same as the other at line 210
        /// </summary>
        /// <param name="con"></param>
        /// <param name="recipe"></param>
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

        /// <summary>
        /// Instantiates a new Recipe object from the database
        /// </summary>
        /// <param name="reader">The SqlDataReader that reads the data from the database</param>
        /// <returns>The instantiated recipe object</returns>
        private Recipe BuildRecipeObject(SqlDataReader reader)
        {
            Recipe recipe = new Recipe();
            recipe.RecipeId = Guid.Parse(reader.GetString(reader.GetOrdinal("recipeId")));
            recipe.Name = reader.GetString(reader.GetOrdinal("name"));
            recipe.Description = reader.GetString(reader.GetOrdinal("description"));
            recipe.PictureURL = reader.GetString(reader.GetOrdinal("pictureURL"));
            recipe.Time = reader.GetInt32(reader.GetOrdinal("time"));
            recipe.PortionNum = reader.GetInt32(reader.GetOrdinal("portionNum"));
            recipe.Author = _userDatabaseAccess.GetUserById(Guid.Parse(reader.GetString(reader.GetOrdinal("authorId"))));
            return recipe;
        }

        /// <summary>
        /// Instantioates a new Ingredient object
        /// </summary>
        /// <param name="reader">The SqlDataReader that reads the data from the database</param>
        /// <returns>The instantiated Ingerdient object</returns>
        private Ingredient BuildIngredientObject(SqlDataReader reader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.name = reader.GetString(reader.GetOrdinal("name"));
            ingredient.amount = reader.GetInt32(reader.GetOrdinal("amount"));
            ingredient.unit = reader.GetString(reader.GetOrdinal("unit"));
            return ingredient;
        }

        /// <summary>
        /// Instantioates a new Instruction object
        /// </summary>
        /// <param name="reader">The SqlDataReader that reads the data from the database</param>
        /// <returns>The instantiated Instruction object</returns>
        private Instruction BuildInstructionObject(SqlDataReader reader)
        {
            Instruction instruction = new Instruction();
            instruction.Step = reader.GetInt32(reader.GetOrdinal("step"));
            instruction.Description = reader.GetString(reader.GetOrdinal("description"));
            return instruction;
        }
        
        /// <summary>
        /// Builds a simplefied recipe
        /// </summary>
        /// <param name="recipeReader">Sql Data reader with the data</param>
        /// <returns>Simplified Recipe</returns>
        private static Recipe BuildRecipeSimplified(SqlDataReader recipeReader)
        {
            Recipe simplefiedRecipe;
            simplefiedRecipe = new Recipe();
            simplefiedRecipe.RecipeId = Guid.Parse(recipeReader.GetString(recipeReader.GetOrdinal("recipeId")));
            simplefiedRecipe.Name = recipeReader.GetString(recipeReader.GetOrdinal("name"));
            simplefiedRecipe.Description = recipeReader.GetString(recipeReader.GetOrdinal("description"));
            simplefiedRecipe.PictureURL = recipeReader.GetString(recipeReader.GetOrdinal("pictureUrl"));
            simplefiedRecipe.Time = recipeReader.GetInt32(recipeReader.GetOrdinal("time"));
            return simplefiedRecipe;
        }
    }
}