import axios from 'axios';
import React, { useEffect, useState } from 'react';
import RecipeService from '../../service/recipeService';
import { Ingredient, Instruction, Recipe } from '../../types';
import style from './GetRecipe.module.css';

const GetRecipe: React.FC = () => {
  const [recipe, setRecipe] = useState<any>([]);
  const [ingredients, setIngredients] = useState<any>([]);
  const [instructions, setInstructions] = useState<any>([]);

  const getSingleData = async () => {
    const response = await RecipeService.getRecipe(window.location.pathname.split('/')[2]);
    const data = response.data;
    setRecipe(data);
    setIngredients(data.ingredients);
    setInstructions(data.instructions);
  };

  useEffect(() => {
    getSingleData();
  }, []);

  return (
    <div>
      <div className={style.recipeImg}>
        <img src={recipe.pictureURL} alt="" />
      </div>
      <div className={style.container}>
        <h2>{recipe.name}</h2>
        <h5>Preparation time: {recipe.time}min</h5>
        <p>{recipe.description}</p>
        <h5>published by: author</h5> {/** TODO: change this to author.name  */}
        <h5>Number of portions: {recipe.portionNum}</h5>
        <h3>Ingredients:</h3>
        {ingredients.map((ingredient: Ingredient) => (
          <div key={ingredient.name}>
            <div className={style.ingredientContainer}>
              <p className={style.ingredientChild}>{ingredient.name}:</p>
              <p className={style.ingredientChild}>
                {ingredient.amount}
                {ingredient.unit}
              </p>
            </div>
          </div>
        ))}
        {instructions.map((instruction: Instruction) => (
          <div className={style.instruction} key={instruction.step}>
            <h4>Step {instruction.step}:</h4>
            <p>{instruction.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default GetRecipe;
