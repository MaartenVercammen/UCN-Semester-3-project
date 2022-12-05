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

export interface SwipedRecipe{
  authorId: string;
  recipeId: string;
  isLiked: boolean;
}

export interface User{
  userId: string;
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  address: string;
  role: Role;
}

export enum Role {
  USER = 'USER',
  VERIFIEDUSER = 'VERIFIEDUSER',
  ADMIN = 'ADMIN'
}


export interface BambooSession{
  SessionId: string;
  Host: User;
  Address: string;
  Recipe: Recipe;
  Description: string;
  DateTime : string;
  SlotsNumber : number;
  Attendees: User[];
}
