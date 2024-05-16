interface Recipe {
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
interface Ingredient {
  Quantity: number;
  Unit: Units;
  Name: string;
}

interface Category {
  Name: string;
  Recipes: Recipe[];
}

interface Review {
  Id: string;
  UserId: string;
  User: User;
  Text: string;
  CreatedDate: Date;
  ModifiedDate: Date;
  RecipeId: string;
  Recipe: Recipe;
}
interface RecipeLikes {
  RecipeId: string;
  Recipe: Recipe;
  LikeCount: number;
  ClickCount: number;
}

interface User {
  id: string;
  UserRecipes: Recipe[];
  LikedRecipes: Recipe[];
  Reviews: Review[];
}

enum Units {
  Kilogram = 0,
  Gram = 1,
  Litre = 2,
  Millilitre = 3,
  Unit = 4,
  Tablespoon = 5,
  Teaspoon = 6,
}
