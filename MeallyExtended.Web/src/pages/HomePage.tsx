import { useEffect, useState } from "react";
import { useRecipesBrowse } from "@/hooks/useRecipesBrowse";
import { useRecipesPopular } from "@/hooks/useRecipesPopular";
import { Link } from "react-router-dom";
import Header from "@/components/ui/header";
import RecipeGrid from "../components/ui/RecipeGrid";
import placeholer from "@/assets/placeholder.png";

const HomePage: React.FC = () => {
  const {
    browseRecipes,
    loading: browseLoading,
    error: browseError,
    recipes,
  } = useRecipesBrowse();
  const {
    fetchPopularRecipes,
    loading: popularLoading,
    error: popularError,
    popularRecipes,
  } = useRecipesPopular();
  const [page] = useState(1);
  const [pageSize] = useState(5);

  useEffect(() => {
    browseRecipes(page, pageSize);
    fetchPopularRecipes(4);
  }, [page, pageSize]);

  if (browseLoading || popularLoading) {
    return <div>Loading...</div>;
  }

  if (browseError || popularError) {
    return <div>Error: {browseError || popularError}</div>;
  }

  return (
    <>
      <div id="index-page" className="h-screen">
        <Header />
        <div
          className="flex flex-col gap-4 justify-center bg-primary-background items-center"
          style={{ padding: 40 }}
        >
          <h2 className="text-3xl font-regular text-white">
            Popular recipes today
          </h2>
          <div className="grid grid-cols-4 gap-10">
            {popularRecipes.map((recipe) => (
              <Link to={`/recipe/${recipe.id}`} key={recipe.id}>
                <div className="flex flex-col items-center p-4">
                  <div
                  >
                    <img
                      src={recipe.imageUrl || placeholer}
                      alt={recipe.title}
                      width={200}
                      height={200}  
                    />
                  </div>
                  <p className="text-sm">{recipe.duration} minutes</p>
                  <p className="text-sm">
                    {recipe.categories.length !== 0
                      ? recipe.categories[0].name
                      : ""}
                  </p>
                  <p className="text-lg">{recipe.title}</p>
                </div>
              </Link>
            ))}
          </div>
        </div>
        <div
          className="flex flex-col gap-4 justify-center items-center"
          style={{ padding: 40 }}
        >
          <h2 className="text-3xl font-regular">All recipes</h2>
          <RecipeGrid recipes={recipes} columns={5} />
          <Link to="/recipes" className="mt-4 text-center">
            Browse All Recipes
          </Link>
        </div>
      </div>
    </>
  );
};

export default HomePage;
