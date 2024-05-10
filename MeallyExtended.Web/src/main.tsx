import React from 'react'
import ReactDOM from 'react-dom/client'
import IndexPage from './pages/IndexPage.tsx'
import './index.css'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import ErrorPage from './pages/ErrorPage.tsx';

const routes = createBrowserRouter([
  {
    // This is the index page
    path: "/",
    element: <IndexPage />,
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
