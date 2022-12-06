using RecipesData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Database
{
    public interface ISwipedRecipeAccess
    {
        SwipedRecipe GetSwipedRecipeById(Guid id, Guid user);

        List<SwipedRecipe> GetSwipeRecipesByUser(Guid userId);
        List<SwipedRecipe> GetLikedByUser(Guid userId);

        SwipedRecipe CreateSwipedRecipe(SwipedRecipe swipedRecipe);

        bool DeleteSR(Guid id, Guid userIds);
    }
}