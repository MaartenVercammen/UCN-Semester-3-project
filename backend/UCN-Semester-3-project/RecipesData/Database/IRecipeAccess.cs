﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesData.Database
{
    public interface IRecipeAccess
    {
        Recipe GetRecipeById(Guid id);

        List<Recipe> GetRecipes();

        int CreateRecipe(Recipe recipe);

        bool UpdateRecipe(Recipe recipe);

        bool DeleteRecipe(int id);
    }
}
