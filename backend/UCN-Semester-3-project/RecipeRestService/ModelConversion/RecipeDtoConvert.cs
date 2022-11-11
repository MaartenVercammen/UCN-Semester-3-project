using RecipeRestService.DTO;
using RecipesData.Model;
using System;

namespace RecipeRestService.ModelConversion
{
    public class RecipeDtoConvert
    {
        public static List<RecipeDto>? FromRecipeCollection(List<Recipe> inRecipes)
        {
            return null;
        }
        public static RecipeDto? FromRecipe(Recipe inRecipe)
        {
            return new RecipeDto(inRecipe.RecipeId, inRecipe.Name, inRecipe.Description, inRecipe.PictureURL, inRecipe.Time, inRecipe.PortionNum, inRecipe.Author.UserId);
        }
        public static Recipe? ToRecipe(RecipeDto inDto)
        {
            //TODO change user to get request to get ID;
            return new Recipe(inDto.RecipeId, inDto.Name, inDto.Description, inDto.PictureURL, inDto.Time, inDto.PortionNum, new User(Guid.Parse("00000000-0000-0000-0000-000000000000"), "mail", "mark", "mark", "pass", "street", Role.USER));
        }
    }
}
