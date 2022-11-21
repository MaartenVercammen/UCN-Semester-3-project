import userEvent from '@testing-library/user-event';
import axios from 'axios';
import { json } from 'stream/consumers';
import { instance } from '../axios';
import { Recipe, SwipedRecipe } from '../types';

const createRecipe = (recipe: Recipe) => instance.post('/Recipes', JSON.stringify(recipe));
const getRecipe = (recipeId: string) => instance.get<Recipe>('/Recipes/' + recipeId);
const getRecipes = () => instance.get<Recipe[]>('/Recipes');
const getRandomRecipe = () => instance.get<Recipe>('/Random');
<<<<<<< HEAD
const swipeRecipe = (swipedRecipe: SwipedRecipe) => instance.post('/SwipedRecipe', JSON.stringify(swipedRecipe));
=======
const deleteRecipe = (recipeId: string) => instance.delete('/Recipes/' + recipeId);
>>>>>>> dev

const RecipeService = {
  createRecipe,
  getRecipe,
  getRecipes,
  getRandomRecipe,
<<<<<<< HEAD
  swipeRecipe,
=======
  deleteRecipe
>>>>>>> dev
};

export default RecipeService;
