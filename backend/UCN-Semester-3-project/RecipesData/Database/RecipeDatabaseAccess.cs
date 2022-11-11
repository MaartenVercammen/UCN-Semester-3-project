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

        public Recipe GetRecipe(int id)
        {
            throw new NotImplementedException();
        }

        public List<Recipe> GetRecipes()
        {
            throw new NotImplementedException();
        }

        public Guid CreateRecipe(Recipe recipe)
        {
            Guid outid = Guid.Empty;

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
                catch(Exception ex)
                {
                    transaction.Rollback();
                }
                
            }
            return outid;
        }

        public void UpdateRecipe(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecipe(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}
