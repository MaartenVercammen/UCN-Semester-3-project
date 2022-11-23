import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import RecipeService from '../../service/recipeService';
import { Ingredient, Instruction, Recipe } from '../../types';
import style from './GetRecipe.module.css';

const GetRecipe: React.FC = () => {
  const [recipe, setRecipe] = useState<any>([]);
  const [ingredients, setIngredients] = useState<any>([]);
  const [instructions, setInstructions] = useState<any>([]);

  const navigation = useNavigate();

  const getSingleData = async () => {
    const response = await RecipeService.getRecipe(window.location.pathname.split('/')[2]);
    const data = response.data;
    setRecipe(data);
    setIngredients(data.ingredients);
    setInstructions(data.instructions);
  };

  const deleteRecipe = async () => {
    if (confirm('Are you sure you want to delete this recipe?')) {
      await RecipeService.deleteRecipe(recipe.recipeId);
      navigation('/app');
    }
  };

  // TODO: implement edit recipe
  const editRecipe = async () => {
    window.alert('not implemented yet');
  };

  useEffect(() => {
    getSingleData();
  }, []);

  return (
    <div className={style.recipeContent}>
      <div className={style.recipeImg}>
        <img src={recipe.pictureURL} alt="" />
      </div>
      <h1>{recipe.name}</h1>
      <div className={style.container}>
        <h4>Preparation time: {recipe.time}min</h4>
        <p>{recipe.description}</p>
        <h4>published by: author</h4> {/** TODO: change this to author.name  */}
        <h4>Number of portions: {recipe.portionNum}</h4>
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
      <div className={style.buttonContainer}>
        <button onClick={(e) => editRecipe()} className={style.editBtn}>edit</button>
        <button onClick={(e) => deleteRecipe()} className={style.deleteBtn}>
          delete
        </button>{' '}
      </div>
    </div>
  );
};

export default GetRecipe;
