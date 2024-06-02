import React, { useEffect, useState } from "react";
import { RecipeViewModel } from "@/models/models";
import apiBaseUrl from "../../API/apiConfig";
import axios, { AxiosError, AxiosResponse } from "axios";
import RecipeGrid from "@/components/ui/RecipeGrid";
import Header from "@/components/ui/header";

function LikeRecipeComponent() {
  const [recipes, setRecipes] = useState<Array<RecipeViewModel>>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(0);

  useEffect(() => {
    fetchRecipes(currentPage);
  }, []);

  useEffect(() => {
    fetchRecipes(currentPage);
  }, [currentPage]);

  const fetchRecipes = (page: number) => {
    setLoading(true);
    axios
      .get(`${apiBaseUrl}/api/User/favorites?pageNo=${page}&pageSize=5`, {
        withCredentials: true,
      })
      .then((response: AxiosResponse<RecipeViewModel[]>) => {
        if (response.status === 204) {
          setRecipes([]);
          setLoading(false);
          return;
        }
        setRecipes(response.data.data);
        setLoading(false);
      })
      .catch((error: AxiosError) => {
        console.error(error);
        setLoading(false);
      });
  };

  return (
    <div>
      <Header setIsLoggedIn={setIsLoggedIn} />
      <h1 className="ml-4 mt-4 text-4xl font-bold">Favorites</h1>
      {loading && <p>Loading...</p>}
      <div className="recipe-container">
        {!loading && isLoggedIn && recipes.length === 0 && (
          <h1>No favorite recipes found</h1>
        )}
        {!loading && recipes.length > 0 && (
          <RecipeGrid
            recipes={recipes}
            columns={5}
            currentPage={currentPage}
            totalPages={totalPages}
            fetchPage={setCurrentPage}
            pagination={true}
          />
        )}
      </div>
    </div>
  );
}

export default LikeRecipeComponent;
