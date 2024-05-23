import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import ErrorPage from "./pages/ErrorPage.tsx";
import RecipePage from "./pages/RecipePage.tsx";
import RecipeForm from "./pages/RecipeFormPage.tsx";
import RecipeLikePage from "./pages/RecipeLikePage.tsx";
import SearchRecipesComponent from "./pages/RecipeSearchPage.tsx";
import RecipeSuggestionComponent from "./pages/RecipeSuggestionPage.tsx";
import HomePage from "./pages/HomePage.tsx";
import LoginPage from "./pages/LoginPage.tsx";
import { Toaster } from "./components/ui/toaster.tsx";
import Header from "./components/ui/header.tsx";
import BrowsePage from "./pages/BrowsePage.tsx";

const routes = createBrowserRouter([
  {
    // This is the index page
    path: "/",
    element: <HomePage />,
  },
  {
    path: "/recipes",
    element: <BrowsePage />,
  },
  {
    path: "/recipeForm",
    element: <RecipeForm />,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/recipe/:recipe_id",
    element: <RecipePage />,
  },
  {
    path: "/recipe/search",
    element: <SearchRecipesComponent />,
  },
  {
    path: "/recipe/suggestion",
    element: <RecipeSuggestionComponent />,
  },
  {
    path: "/Recipe/Likes",
    element: <RecipeLikePage />,
  },
  {
    // This is the 404 page
    path: "*",
    element: <ErrorPage />,
  },
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <Toaster />
    <RouterProvider router={routes} />
  </React.StrictMode>
);
