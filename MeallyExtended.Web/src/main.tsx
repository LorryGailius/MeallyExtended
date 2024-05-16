import React from 'react'
import ReactDOM from 'react-dom/client'
import IndexPage from './pages/IndexPage.tsx'
import './index.css'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import ErrorPage from './pages/ErrorPage.tsx';
import HomePage from './pages/HomePage.tsx';
import RecipePage from './pages/RecipePage.tsx';
import LoginPage from './pages/LoginPage.tsx';

const routes = createBrowserRouter([
  {
    // This is the index page
    path: "/",
    element: <HomePage />,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/recipes/:recipe_id",
    element: <RecipePage />,
  },
  {
    // This is the 404 page
    path: "*",
    element: <ErrorPage />,
  }
]);

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={routes} />
  </React.StrictMode>,
)
