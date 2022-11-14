export interface Recipe{
  recipeId: string;
  name: string;
  description: string;
  pictureURL: string;
  time: number;
  portionNum: number;
  author: string;
  ingredients: Ingredient[];
  instructions: Instruction[];
}

export interface Ingredient{
  name: string;
  amount: number;
  unit: string;
}

export interface Instruction{
  step: number;
  description: string;
}
