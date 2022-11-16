using RecipeRestService.DTO;
using RecipesData.Model;
using System;

namespace RecipeRestService.ModelConversion
{
    public class RecipeDtoConvert
    {
        public static List<RecipeDto>? FromRecipeCollection(List<Recipe> inRecipes)
        {
            List<RecipeDto> recipeDTOs = null;
            if (inRecipes != null)
            {
                recipeDTOs = new List<RecipeDto>();
                RecipeDto tempDto;
                foreach (Recipe recipe in inRecipes)
                {
                    if (recipe != null)
                    {
                        tempDto = FromRecipe(recipe);
                        recipeDTOs.Add(tempDto);
                    }
                }
            }
            return recipeDTOs;
        }
        public static RecipeDto? FromRecipe(Recipe inRecipe)
        {
            RecipeDto? recipeDTO = null;
            if (inRecipe != null)
            {
                // TODO: change guid string to author id once author is implemented
                recipeDTO = new RecipeDto(inRecipe.RecipeId, inRecipe.Name, inRecipe.Description, inRecipe.PictureURL, inRecipe.Time, inRecipe.PortionNum, new Guid("34dc5363-e96d-4f64-a46e-3deb150a59c0"));
                recipeDTO.Ingredients = inRecipe.Ingredients;
                recipeDTO.Instructions = inRecipe.Instructions;
            }
            return recipeDTO;
        }
        
        public static Recipe? ToRecipe(RecipeDto inDto)
        {
            //TODO change user to get request to get ID;
            return new Recipe(inDto.RecipeId, inDto.Name, inDto.Description, inDto.PictureURL, inDto.Time, inDto.PortionNum, new User(Guid.Parse("00000000-0000-0000-0000-000000000000"), "mail", "mark", "mark", "pass", "street", Role.USER));
        }
    }
}