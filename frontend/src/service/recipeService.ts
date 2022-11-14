import axios from "axios";
import { instance } from "../axios";
import { Recipe } from "../types"

const createRecipe = (recipe: Recipe) => instance.post("/Recipes", JSON.stringify(recipe))

const RecipeService = {
  createRecipe,
}

export default RecipeService;
