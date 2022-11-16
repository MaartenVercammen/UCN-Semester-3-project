﻿using RecipesData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipesData.Model;

namespace RecipesData.Database
{
    public interface IRecipeAccess
    {
        Recipe GetRecipeById(Guid id);

        List<Recipe> GetRecipes();

        Guid CreateRecipe(Recipe recipe);

        bool UpdateRecipe(Recipe recipe);

        bool DeleteRecipe(int id);

        List<Guid> GetGuids();
    }
}
