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
            Guid id = Guid.Empty;

            string queryRecipe = "insert into recipe (name, description, pictureURL, time, portionNum) output INSERTED.id values (@name, @description, @pictureURL, @time, @portionNum)";
            string queryIngredient = "insert into ingredient(name, amount, unit, recipeId) output INSERTED.id values(@name, @amount, @unit, @recipeId)";
            string querystruction = "insert into ingredient(step, description, recipeId) output INSERTED.id values(@step, @description, @recipeId)";


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
                    cmd.Parameters.AddWithValue("name", recipe.Name);
                    cmd.Parameters.AddWithValue("description", recipe.Description);
                    cmd.Parameters.AddWithValue("pictureURL", recipe.PictureURL);
                    cmd.Parameters.AddWithValue("time", recipe.Time);
                    cmd.Parameters.AddWithValue("portionNum", recipe.PortionNum);
                    id = (Guid)cmd.ExecuteScalar();

                    //create ingredients
                    for(int i = 0; i < recipe.Ingredients.Count; i++)
                    {
                        Ingredient ingredient = recipe.Ingredients[i];
                        cmd.CommandText = queryIngredient;
                        cmd.Parameters.AddWithValue("name", ingredient.name);
                        cmd.Parameters.AddWithValue("amount", ingredient.amount);
                        cmd.Parameters.AddWithValue("unit", ingredient.unit);
                        cmd.Parameters.AddWithValue("recipeId", id);
                        cmd.ExecuteNonQuery();
                    }

                    //create inestructions
                    for (int i = 0; i < recipe.Instructions.Count; i++)
                    {
                        Instruction instruction = recipe.Instructions[i];
                        cmd.CommandText = querystruction;
                        cmd.Parameters.AddWithValue("step", instruction.Step);
                        cmd.Parameters.AddWithValue("description", instruction.Description);
                        cmd.Parameters.AddWithValue("recipeId", id);
                        cmd.ExecuteNonQuery();
                    }

                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                }
                
            }
            return id;
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
