using RecipesData.Model;

namespace RecipeRestService.DTO
{
    public class SwipedRecipeDto
    {
        public Guid UserId { get; set; }
        public Guid RecipeId { get; set; }
        public bool IsLiked { get; set; }

        public SwipedRecipeDto(Guid userId, Guid recipeId, bool isLiked)
        {
            this.UserId = userId;
            this.RecipeId = recipeId;
            this.IsLiked = isLiked;
        }

    }
}

