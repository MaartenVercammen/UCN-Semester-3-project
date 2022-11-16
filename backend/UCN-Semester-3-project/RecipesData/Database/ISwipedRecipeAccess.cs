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
        SwipedRecipe GetSRById(Guid id);

        List<SwipedRecipe> GetSRByUser(Guid userId);
        List<SwipedRecipe> GetLikedByUser(Guid userId);

        SwipedRecipe CreateSR(SwipedRecipe swipedRecipe);

        bool DeleteSR(Guid id);
    }
}
