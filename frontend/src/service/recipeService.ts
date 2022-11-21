import axios from 'axios';
import { json } from 'stream/consumers';
import { instance } from '../axios';
import { Recipe } from '../types';

const createRecipe = (recipe: Recipe) => instance.post('/Recipes', JSON.stringify(recipe));
const getRecipe = (recipeId: string) => instance.get<Recipe>('/Recipes/' + recipeId);
const getRecipes = () => instance.get<Recipe[]>('/Recipes');
const getRandomRecipe = () => instance.get<Recipe>('/Random');
const deleteRecipe = (recipeId: string) => instance.delete('/Recipes/' + recipeId);

const RecipeService = {
  createRecipe,
  getRecipe,
  getRecipes,
  getRandomRecipe,
  deleteRecipe
};

export default RecipeService;
