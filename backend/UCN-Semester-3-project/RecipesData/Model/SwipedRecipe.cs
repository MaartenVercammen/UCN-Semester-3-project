using System;

namespace RecipesData.Model
{
	

public class SwipedRecipe
{
	public Guid UserId {get;set;}
	public Guid RecipeId {get;set;}
	public bool IsLiked {get;set;}

	public SwipedRecipe(Guid userId, Guid recipeId, bool isLiked)
	{
		this.UserId = userId;
		this.RecipeId = recipeId;
		this.IsLiked = isLiked;
	}

	public SwipedRecipe()
    {
        
    }
}
}