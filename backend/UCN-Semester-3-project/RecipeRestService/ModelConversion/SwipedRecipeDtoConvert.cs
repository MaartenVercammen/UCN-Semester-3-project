using RecipeRestService.DTO;
using RecipesData.Model;
using System;

namespace RecipeRestService.ModelConversion
{
    public class SwipedRecipeDtoConvert
    {
        public static SwipedRecipeDto? FromSwipedRecipe(SwipedRecipe inSwipedRecipe)
        {
            SwipedRecipeDto? swipedRecipeDTO = null;
            if (inSwipedRecipe != null)
            {
                swipedRecipeDTO = new SwipedRecipeDto(inSwipedRecipe.UserId, inSwipedRecipe.RecipeId, inSwipedRecipe.IsLiked);
            }
            return swipedRecipeDTO;
        }
        
        public static SwipedRecipe? ToSWRecipe(SwipedRecipeDto inDto)
        {
            return new SwipedRecipe(inDto.UserId, inDto.RecipeId, inDto.IsLiked);
        }
    }
}