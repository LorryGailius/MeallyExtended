import React, { useEffect, useState } from "react";
import { useSearchRecipes } from "@/hooks/useSearchRecipes";
import RecipeGrid from "../components/ui/RecipeGrid";
import { useRecipesBrowse } from "@/hooks/useRecipesBrowse";
import Header from "@/components/ui/header";
import { RecipeViewModel } from "@/models/models";

const BrowsePage: React.FC = () => {
  const [searchQuery, setSearchQuery] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 10;
  const {
    searchRecipes,
    loading: searchLoading,
    error: searchError,
    response: searchResponse,
  } = useSearchRecipes();
  const {
    browseRecipes,
    loading: browseLoading,
    error: browseError,
    recipes: browseResponse,
    totalPages: browseTotalPages,
  } = useRecipesBrowse();

  useEffect(() => {
    if (searchQuery && searchQuery.trim() !== "") {
      searchRecipes(searchQuery, currentPage, pageSize);
    } else {
      browseRecipes(currentPage, pageSize);
    }
  }, [searchQuery, currentPage, pageSize]);

  const loading = browseLoading;
  const error = browseError;
  const [recipes, setRecipes] = useState<RecipeViewModel[]>([]);
  const totalPages = searchResponse?.totalPages || browseTotalPages;

  useEffect(() => {
    if (searchResponse?.length === 0 && searchQuery.trim() !== "") {
      setRecipes([]);
    } else if (searchQuery.trim() === "") {
      setRecipes(browseResponse);
    } else {
      setRecipes(searchResponse?.data || []);
    }
  }, [searchResponse, browseResponse, searchQuery]);

  if (loading || browseLoading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <Header />
      <div className="flex justify-end p-4">
        <input
          type="text"
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
          placeholder="Search recipes..."
          className="border-2 border-gray-300 bg-white h-10 px-5 pr-16 rounded-lg text-sm focus:outline-none"
          autoFocus
        />
      </div>
      {loading ? (
        <div>Loading...</div>
      ) : error ? (
        <div>Error: {error}</div>
      ) : recipes.length === 0 && searchQuery.trim() !== "" ? (
        <div>Nothing found</div>
      ) : (
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
  );
};

export default BrowsePage;
