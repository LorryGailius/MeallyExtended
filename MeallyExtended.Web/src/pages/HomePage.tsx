import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useRecipesBrowse } from "@/hooks/useRecipesBrowse";
import { Button } from "@/components/ui/button";
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";
import { useRecipesPopular } from "@/hooks/useRecipesPopular";
import { Link } from "react-router-dom";
import Header from "@/components/ui/header";

const HomePage: React.FC = () => {
  const {
    browseRecipes,
    loading: browseLoading,
    error: browseError,
    recipes,
    totalPages,
  } = useRecipesBrowse();
  const {
    fetchPopularRecipes,
    loading: popularLoading,
    error: popularError,
    popularRecipes,
  } = useRecipesPopular();
  const [page, setPage] = useState(1);
  const [pageSize] = useState(15);
  const history = useNavigate();

  useEffect(() => {
    browseRecipes(page, pageSize);
    fetchPopularRecipes(5);
  }, [page, pageSize]);

  // if (browseLoading || popularLoading) {
  //   return <div>Loading...</div>;
  // }

  // if (browseError || popularError) {
  //   return <div>Error: {browseError || popularError}</div>;
  // }

  return (
    <>
      <div id="index-page" className="h-screen">
        <Header />
        {/* <div
          className="flex justify-between items-center"
          style={{ padding: 40 }}
        >
          <div>
            <h1 className="text-4xl font-bold">Meally</h1>
            <p className="text-lg">Your meal planner</p>
          </div>
          <div>
            <Button
              className="bg-primary-background mr-2"
              onClick={() => history("/create-recipe")}
            >
              Create New Recipe
            </Button>
            <Button
              className="mr-2 bg-accent"
              onClick={() => history("/login")}
            >
              Login
            </Button>
            <Button
              className="mr-2 bg-accent"
              onClick={() => history("/register")}
            >
              Register
            </Button>
          </div>
        </div>
        <div
          className="flex flex-col gap-4 justify-center bg-primary-background items-center"
          style={{ padding: 40 }}
        >
          <h2 className="text-3xl font-regular text-white">
            Popular recipes today
          </h2>
          <div className="grid grid-cols-5 gap-10">
            {popularRecipes.map((recipe) => (
              <Link to={`/recipes/${recipe.id}`} key={recipe.id}>
                <div className="flex flex-col items-center">
                  <div>
                    <img
                      src={recipe.image || "https://via.placeholder.com/150"}
                      alt={recipe.title}
                      style={{ maxWidth: "100%", maxHeight: "100%" }}
                    />
                  </div>
                  <p className="text-sm">{recipe.duration} minutes</p>
                  <p className="text-sm">
                    {recipe.categories.length !== 0
                      ? recipe.categories[0].Name
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
          <div className="grid grid-cols-5 gap-4">
            {recipes.map((recipe) => (
              <Link to={`/recipes/${recipe.id}`} key={recipe.id}>
                <div className="flex flex-col items-center p-4">
                  <div>
                    <img
                      src={recipe.image || "https://via.placeholder.com/150"}
                      alt={recipe.title}
                      style={{ maxWidth: "100%", maxHeight: "100%" }}
                    />
                  </div>
                  <p className="text-sm">{recipe.duration} minutes</p>
                  <p className="text-sm">
                    {recipe.categories.length !== 0
                      ? recipe.categories[0].Name
                      : ""}
                  </p>
                  <p className="text-lg">{recipe.title}</p>
                </div>
              </Link>
            ))}
          </div>
        </div>
        {totalPages > 1 && (
          <Pagination>
            <PaginationContent>
              <PaginationItem>
                <PaginationPrevious
                  href="#"
                  onClick={() => setPage((prev) => Math.max(prev - 1, 1))}
                />
              </PaginationItem>
              {Array.from({ length: totalPages }, (_, i) => (
                <PaginationItem key={i}>
                  <PaginationLink
                    href="#"
                    isActive={i + 1 === page}
                    onClick={() => setPage(i + 1)}
                  >
                    {i + 1}
                  </PaginationLink>
                </PaginationItem>
              ))}
              <PaginationItem>
                <PaginationNext
                  href="#"
                  onClick={() =>
                    setPage((prev) => Math.min(prev + 1, totalPages))
                  }
                />
              </PaginationItem>
            </PaginationContent>
          </Pagination>
        )} */}
      </div>
    </>
  );
};

export default HomePage;
