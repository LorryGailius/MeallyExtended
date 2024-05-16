import { useRecipe } from "@/hooks/useRecipe";
import { useEffect } from "react";
import { useParams } from "react-router";

const RecipePage: React.FC = () => {
  const { recipe_id } = useParams<string>();
  const { getRecipe, loading, error, recipe } = useRecipe();

  useEffect(() => {
    getRecipe(recipe_id ?? "");
  }, [recipe_id]);

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div id="recipe-page" className="h-screen">
      Recipe Page
      <div>
        {loading && <div>Loading...</div>}
        {error && <div>Error: {error}</div>}
        {recipe && (
          <div>
            <h1>{recipe.title}</h1>
            <p>{recipe.description}</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default RecipePage;
