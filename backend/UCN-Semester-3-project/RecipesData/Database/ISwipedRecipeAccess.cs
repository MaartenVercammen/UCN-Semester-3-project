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
        SwipedRecipe GetSwipedRecipeById(Guid id);

        List<SwipedRecipe> GetSwipedRecipesByUser(Guid userId);
        List<SwipedRecipe> GetLikedByUser(Guid userId);

        SwipedRecipe CreateSwipedRecipe(SwipedRecipe swipedRecipe);

        bool DeleteSR(Guid id);
    }
}
