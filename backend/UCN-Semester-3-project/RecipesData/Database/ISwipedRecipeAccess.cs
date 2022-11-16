using RecipesData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipesData.Model;

namespace RecipesData.Database
{
    public interface ISwipedRecipeAccess
    {
        SwipedRecipe GetSwipedRecipeById(Guid id);

        void CreateSwipedRecipe(SwipedRecipe swipedRecipe);

        void DeleteSwipedRecipe(Guid id);
    }
}
