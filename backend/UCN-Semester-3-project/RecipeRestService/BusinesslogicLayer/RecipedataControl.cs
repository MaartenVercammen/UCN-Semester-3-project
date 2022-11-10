﻿using RecipesData.DatabaseLayer;

namespace RecipeRestService.BusinesslogicLayer
{
    public class RecipedataControl : IRecipeData
    {
        IRecipeAccess _RecipeAccess;
        public RecipedataControl(IConfiguration inConfiguration)
        {
            _RecipeAccess = new RecipeDatabaseAccess(inConfiguration);
        }

        //implement IRecipeData
    }
}