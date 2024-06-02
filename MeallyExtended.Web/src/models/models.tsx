export interface Recipe {
  id: string;
  title: string;
  ingredients?: Ingredient[];
  image?: string;
  description?: string;
  duration: number;
  instructions: string;
  categories: Category[];
  reviews: Review[];
  recipeLikes: RecipeLikes;
  usersLiked: User[];
  userId?: string;
  user: User;
}

export interface Ingredient {
  quantity: number;
  unit: Units;
  name: string;
}

export interface Category {
  name: string;
}

export interface Review {
  id: string;
  userEmail: string;
  text: string;
  createdDate: Date;
  recipeId: string;
  modifiedDate: Date;
  version: string;
}

export interface RecipeLikes {
  recipeId: string;
  recipe: Recipe;
  likeCount: number;
  clickCount: number;
}

export interface User {
  id: string;
  userRecipes: Recipe[];
  likedRecipes: Recipe[];
  reviews: Review[];
}

export enum Units {
  kg = 0,
  g = 1,
  L = 2,
  ml = 3,
  piece = 4,
  teaspoon = 5,
  tablespoon = 6,
  toTase = 7,
}

export interface RecipeViewModel {
  id: string;
  title: string;
  ingredients: Ingredient[];
  description?: string;
  imageUrl?: string;
  duration: number;
  instructions: string;
  categories: Category[];
  reviews: Review[];
  likesCount: number;
  userEmail: string;
  version: string;
  isFavorite: boolean;
}
