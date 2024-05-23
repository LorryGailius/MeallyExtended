import Header from "@/components/ui/header";
import axios, { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import apiBaseUrl from "../../API/apiConfig";
import { useParams } from "react-router";
import { Ingredient, RecipeViewModel, Review, Units } from "@/models/models";
import { Button } from "@/components/ui/button";
import { Heart, Pencil } from "lucide-react";
import { useToast } from "@/components/ui/use-toast";
import ReviewComponent from "@/components/ui/comment";
import { NavigateFunction, useNavigate } from "react-router-dom";
import placeholder from "@/assets/placeholder.png";

const RecipePage: React.FC = () => {
  const { recipe_id } = useParams<string>();
  const [loading, setLoading] = useState<boolean>(true);
  const [commentsLoading, setCommentsLoading] = useState<boolean>(true);
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [recipe, setRecipe] = useState<RecipeViewModel | null>(null);
  const [recipeReviews, setRecipeReviews] = useState<Review[]>([]);
  const [showMore, setShowMore] = useState<boolean>(true);
  const [userInfo, setUserInfo] = useState<string>("");
  const nav: NavigateFunction = useNavigate();
  const { toast } = useToast();

  useEffect(() => {
    if (recipe_id) {
      fetchRecipe(recipe_id);
      fetchComments(recipe_id);
    }
    setLoading(false);
  }, []);

  const fetchRecipe = (recipeId: string) => {
    axios
      .get(`${apiBaseUrl}/api/Recipe/${recipeId}`, { withCredentials: true })
      .then((response: AxiosResponse<RecipeViewModel>) => {
        setRecipe(response.data);
        setLoading(false);
      })
      .catch((error) => {
        if (error.response.status === 404) {
          nav("/not-found");
        }
      });
  };

  const fetchComments = (
    recipeId: string,
    skip: number = 0,
    append: boolean = false
  ) => {
    axios
      .get(`${apiBaseUrl}/api/Review/${recipeId}?limit=5&skip=${skip}`, {
        withCredentials: true,
      })
      .then((response: AxiosResponse<Review[]>) => {
        if (append) {
          setRecipeReviews((prev) => [...prev, ...response.data]);
          if (response.data.length === 0) {
            setShowMore(false);
          }
        } else {
          setRecipeReviews(response.data);
        }
        setCommentsLoading(false);
      })
      .catch((error) => {
        console.error(error);
      });
  };

  const getMoreComments = () => {
    fetchComments(recipe_id!, recipeReviews.length, true);
  };

  const LikeRecipe = () => {
    if (!isLoggedIn) {
      toast({
        title: "Login required",
        description: "You need to be logged in to like a recipe",
        variant: "destructive",
      });
      return;
    }

    axios
      .post(
        `${apiBaseUrl}/api/Recipe/like?recipeId=${recipe!.id}`,
        {},
        { withCredentials: true }
      )
      .then(() => {
        setRecipe((prev) => {
          if (prev) {
            return { ...prev, isFavorite: !prev.isFavorite };
          }
          return prev;
        });
      })
      .catch((error) => {
        console.error(error);
      });
  };

  useEffect(() => {}, [recipe]);

  return (
    <div id="recipe-page" className="h-screen pb-10">
      <Header setIsLoggedIn={setIsLoggedIn} setUserInfo={setUserInfo} />
      {loading && <div>Loading...</div>}
      {!loading && recipe && (
        <>
          <div className="my-4">
            <div className="w-full bg-primary-background p-10 flex justify-between">
              <div className="flex justify-center gap-24">
                <h1 className="text-4xl font-bold text-white">
                  {recipe?.title}
                </h1>
                <div>
                  <div className="flex gap-4">
                    {recipe?.categories.map((category) => (
                      <div
                        key={category.name}
                        className="bg-accent p-2 rounded-md text-white"
                      >
                        <p className="font-semibold select-none">
                          {category.name}
                        </p>
                      </div>
                    ))}
                  </div>
                </div>
              </div>

              {userInfo !== recipe?.userEmail && (
                <Button className="bg-transparent" onClick={LikeRecipe}>
                  {recipe?.isFavorite ? <Heart fill="white" /> : <Heart />}
                </Button>
              )}

              {isLoggedIn && userInfo === recipe?.userEmail && (
                <Button className="bg-transparent">
                  <Pencil />
                </Button>
              )}
            </div>
          </div>
          <div className="flex justify-between">
            <div className="mx-10">
              <div className="flex gap-24">
                <div className="border-8 border-white shadow-lg w-[400px] h-[400px]">
                  <img
                    src={recipe?.imageUrl? recipe.imageUrl : placeholder}
                    alt={recipe?.title}
                    width={400}
                    height={400}
                  />
                </div>
                <div>
                  <h1 className="text-3xl font-bold mb-4">Ingredients</h1>
                  <ul>
                    {recipe?.ingredients.map((ingredient: Ingredient) => (
                      <li key={ingredient.name}>
                        {ingredient.quantity}{" "}
                        {ingredient.unit != 4
                          ? Units[ingredient.unit].toString()
                          : null}{" "}
                        {ingredient.name}
                      </li>
                    ))}
                  </ul>
                </div>
              </div>
              <div className="my-10 mx-4 max-w-6xl">
                <h1 className="text-3xl font-bold mb-4">Instructions</h1>
                <p>{recipe?.instructions}</p>
              </div>
            </div>
            <div className="max-w-xl w-full">
              <div className="flex flex-col">
                <h1 className="text-3xl font-bold mb-4">Comments</h1>
                <div>
                  {recipeReviews.length === 0 && !commentsLoading && (
                    <div className="text-start">No comments yet</div>
                  )}

                  {commentsLoading && <div>Loading comments...</div>}
                  <div className="pr-10">
                    {recipeReviews.length > 0 && (
                      <div>
                        {recipeReviews.map((review) => (
                          <ReviewComponent key={review.id} review={review} />
                        ))}
                      </div>
                    )}
                    {showMore && recipeReviews.length !== 0 && (
                      <div className="flex justify-center">
                        <Button variant="ghost" onClick={getMoreComments}>
                          Show more
                        </Button>
                      </div>
                    )}

                    {recipeReviews.length > 0 && !showMore && (
                      <div className="text-center">No more comments</div>
                    )}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default RecipePage;
