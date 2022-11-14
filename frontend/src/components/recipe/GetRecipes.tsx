import axios from 'axios';
import React, { useEffect, useState } from 'react';
import RecipeService from '../../service/recipeService';
import { Recipe } from '../../types';

const GetRecipes: React.FC = () => {
  const [recipes, setRecipes] = useState<any>([]);

  const getData = async () => {
    const response = await RecipeService.getRecipes();
    const data = response.data;
    setRecipes(data);
  };

  useEffect(() => {
    getData();
  }, []);

  return (
    <>
      {recipes.map((recipe: Recipe) => (
        <div key={recipe.recipeId}>
          <img src={recipe.pictureURL} alt="" />
          <div>
            <h2>{recipe.name}</h2>
            <h4>{recipe.time}</h4>
            <h4>{recipe.description}</h4>
          </div>
        </div>
      ))}
    </>
  );
};

export default GetRecipes;
