using RecipeRestService.DTO;
using RecipesData.Model;

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
            Guid userId = Guid.Empty;
            RecipeDto? recipeDTO = null;
            if (inRecipe != null)
            {
                if(inRecipe.Author != null) {
                    userId = inRecipe.Author.UserId;
                }
                recipeDTO = new RecipeDto(inRecipe.RecipeId, inRecipe.Name, inRecipe.Description, inRecipe.PictureURL, inRecipe.Time, inRecipe.PortionNum, userId);
                recipeDTO.Ingredients = inRecipe.Ingredients;
                recipeDTO.Instructions = inRecipe.Instructions;
            }
            return recipeDTO;
        }
        
        public static Recipe? ToRecipe(RecipeDto inDto, User author)
        {

            return new Recipe(inDto.RecipeId, inDto.Name, inDto.Description, inDto.PictureURL, inDto.Time, inDto.PortionNum, author);
        }
    }
}